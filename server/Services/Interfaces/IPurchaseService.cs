using server.DTOs.Purchases;

namespace server.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseResponseDto>> GetAllAsync();
        Task<PurchaseResponseDto?> GetByIdAsync(int id);

        Task<PurchaseResponseDto> AddAsync(PurchaseCreateDto createDto);
        Task<PurchaseResponseDto> UpdateAsync(PurchaseUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);

        Task<List<PurchaseResponseDto>> GetByGiftAsync(int giftId);
    }
}
