using server.DTOs.Cart;

namespace server.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemResponseDto>> GetCartAsync(int userId);
        Task<CartItemResponseDto> AddToCartAsync(CartAddDto dto);
        Task<CartItemResponseDto> UpdateQtyAsync(CartUpdateDto dto);
        Task<bool> RemoveAsync(int purchaseId);
        Task<CartCheckoutResponseDto> CheckoutAsync(int userId);
    }
}
