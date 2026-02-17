using server.DTOs;

namespace server.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemResponseDto>> GetCartAsync(int userId);
        Task<CartItemResponseDto> AddToCartAsync(CartAddDto dto, int userId);
        Task<CartItemResponseDto> UpdateQtyAsync(CartAddDto dto, int userId);
        Task<bool> RemoveAsync(int purchaseId);
        Task<CartCheckoutResponseDto> CheckoutAsync(int userId);
    }
}
