using server.DTOs;

namespace server.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemResponseDto>> GetCartAsync(int userId);
        Task<CartItemResponseDto> AddToCartAsync(CartAddDto dto);
        Task<CartItemResponseDto> UpdateQtyAsync(CartAddDto dto);
        Task<bool> RemoveAsync(int purchaseId);
        Task<CartCheckoutResponseDto> CheckoutAsync(int userId);
    }
}
