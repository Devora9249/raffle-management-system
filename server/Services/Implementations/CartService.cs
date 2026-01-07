using server.DTOs;
using server.Models; 
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IPurchaseRepository _repo;

        public CartService(IPurchaseRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CartItemResponseDto>> GetCartAsync(int userId)
        {
            var items = await _repo.GetUserCartAsync(userId);
            return items.Select(ToCartItemDto).ToList();
        }

        public async Task<CartItemResponseDto> AddToCartAsync(CartAddDto dto)
        {
            if (dto.Qty <= 0) throw new ArgumentException("Qty must be greater than 0");

            var purchase = new PurchaseModel
            {
                UserId = dto.UserId,
                GiftId = dto.GiftId,
                Qty = dto.Qty,
                Status = Status.Draft,
                PurchaseDate = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(purchase);
            return ToCartItemDto(created);
        }

        public async Task<CartItemResponseDto> UpdateQtyAsync(CartAddDto dto)
        {
            if (dto.Qty <= 0)
                throw new ArgumentException("Qty must be greater than 0");

            var existing = await _repo.FindDraftByUserAndGift(dto.UserId, dto.GiftId);

            if (existing == null)
                return await AddToCartAsync(dto);

            existing.Qty = dto.Qty;

            var updated = await _repo.UpdateAsync(existing);
            return ToCartItemDto(updated); 
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
            var count = await _repo.CheckoutAsync(userId);

            if (count == 0)
                return new CartCheckoutResponseDto
                {
                    UserId = userId,
                    ItemsCompleted = 0,
                    Message = "Cart is empty"
                };

            return new CartCheckoutResponseDto
            {
                UserId = userId,
                ItemsCompleted = count,
                Message = "Checkout completed successfully"
            };
        }

        private static CartItemResponseDto ToCartItemDto(PurchaseModel p)
            => new CartItemResponseDto
            {
                PurchaseId = p.Id,
                GiftId = p.GiftId,
                Qty = p.Qty,
                AddedAt = p.PurchaseDate
            };
    }
}
