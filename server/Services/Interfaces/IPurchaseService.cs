using server.DTOs;

namespace server.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseResponseDto>> GetAllAsync();
        Task<PurchaseResponseDto?> GetByIdAsync(int id);

        Task<PurchaseResponseDto> AddAsync(int userId, PurchaseCreateDto createDto);
        Task<PurchaseResponseDto> UpdateAsync(int id, PurchaseUpdateDto dto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PurchaseResponseDto>> GetByGiftAsync(int giftId);
        Task<IEnumerable<GiftPurchaseCountDto>> GetPurchaseCountByGiftAsync();
    }
}
