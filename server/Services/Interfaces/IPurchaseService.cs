using server.DTOs;

namespace server.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseResponseDto>> GetAllAsync();
        Task<PurchaseResponseDto?> GetByIdAsync(int id);

        Task<PurchaseResponseDto> AddAsync(PurchaseCreateDto createDto);
     Task UpdateAsync(int id, PurchaseUpdateDto dto);

        Task<bool> DeleteAsync(int id);

        Task<List<PurchaseResponseDto>> GetByGiftAsync(int giftId);
    }
}
