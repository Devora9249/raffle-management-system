using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.Data;

namespace server.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IPurchaseRepository _repo;
        private readonly IRaffleStateService _raffleState;
        private readonly AppDbContext _context;
        public CartService(IPurchaseRepository repo, IRaffleStateService raffleState, AppDbContext context)
        {
            _repo = repo;
            _raffleState = raffleState;
            _context = context;
        }

        public async Task<List<CartItemResponseDto>> GetCartAsync(int userId)
        {
            var items = await _repo.GetUserCartAsync(userId);
            return items.Select(ToCartItemDto).ToList();
        }

        public async Task<CartItemResponseDto> AddToCartAsync(CartAddDto dto, int userId)
        {
            if (_raffleState.Status == RaffleStatus.Finished)
            {
                throw new InvalidOperationException("Raffle has ended");
            }


            if (dto.Qty <= 0) throw new ArgumentException("Qty must be greater than 0");

            var purchase = new PurchaseModel
            {
                UserId = userId,
                GiftId = dto.GiftId,
                Qty = dto.Qty,
                Status = Status.Draft,
                PurchaseDate = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(purchase);
            return ToCartItemDto(created!);
        }

        public async Task<CartItemResponseDto> UpdateQtyAsync(CartAddDto dto, int userId)
        {
            if (_raffleState.Status == RaffleStatus.Finished)
            {
                throw new InvalidOperationException("Raffle has ended");
            }

            if (dto.Qty <= 0)
                throw new ArgumentException("Qty must be greater than 0");

            var existing = await _repo.FindDraftByUserAndGift(userId, dto.GiftId);

            if (existing == null)
                return await AddToCartAsync(dto, userId);

            existing.Qty = dto.Qty;

            var updated = await _repo.UpdateAsync(existing);
            return ToCartItemDto(updated!);
        }

        public async Task<bool> RemoveAsync(int purchaseId)
        {
            var existing = await _repo.GetByIdAsync(purchaseId);
            if (existing == null)
                throw new KeyNotFoundException("Cart item not found");
            ;
            if (existing.Status != Status.Draft)
                throw new InvalidOperationException("Only Draft items can be deleted");

            return await _repo.DeleteAsync(purchaseId);
        }

        public async Task<CartCheckoutResponseDto> CheckoutAsync(int userId)
        {
            var cartItems = await _repo.GetUserCartAsync(userId);

            if (cartItems.Count == 0)
                return new CartCheckoutResponseDto
                {
                    UserId = userId,
                    ItemsCompleted = 0,
                    Message = "Cart is empty"
                };
            foreach (var item in cartItems)
            {
                item.Status = Status.Completed;
                item.PurchaseDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return new CartCheckoutResponseDto
            {
                UserId = userId,
                ItemsCompleted = cartItems.Count,
                Message = "Checkout completed successfully"
            };
        }


        private static CartItemResponseDto ToCartItemDto(PurchaseModel p)
            => new CartItemResponseDto
            {
                PurchaseId = p.Id,
                GiftId = p.GiftId,
                GiftName = p.Gift?.Description ?? string.Empty,
                GiftPrice = p.Gift?.Price ?? 0,
                Qty = p.Qty,
                AddedAt = p.PurchaseDate
            };
    }
}
