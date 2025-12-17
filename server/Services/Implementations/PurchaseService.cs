using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repo;

        public PurchaseService(IPurchaseRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PurchaseResponseDto>> GetAllAsync()
            => (await _repo.GetAllAsync()).Select(ToResponseDto);

        public async Task<PurchaseResponseDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : ToResponseDto(p);
        }

        public async Task<PurchaseResponseDto> AddAsync(PurchaseCreateDto createDto)
        {
            if (createDto.Qty <= 0) throw new ArgumentException("Qty must be greater than 0");

            var purchase = new PurchaseModel
            {
                UserId = createDto.UserId,
                GiftId = createDto.GiftId,
                Qty = createDto.Qty,
                Status = Status.Completed,      // רכישה אמיתית = Completed
                PurchaseDate = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(purchase);
            return ToResponseDto(created);
        } 
public async Task UpdateAsync(int id, PurchaseUpdateDto dto)
{
    var purchase = await _repo.GetByIdAsync(id);
    if (purchase == null) throw new KeyNotFoundException("Purchase not found");

    if (dto.Qty.HasValue)
        purchase.Qty = dto.Qty.Value;

    if (dto.Status.HasValue)
        purchase.Status = dto.Status.Value;

    await _repo.UpdateAsync(purchase);
}


        public Task<bool> DeleteAsync(int id)
            => _repo.DeleteAsync(id);

        public async Task<List<PurchaseResponseDto>> GetByGiftAsync(int giftId)
            => (await _repo.GetByGiftAsync(giftId)).Select(ToResponseDto).ToList();

        private static PurchaseResponseDto ToResponseDto(PurchaseModel p)
            => new PurchaseResponseDto
            {
                Id = p.Id,
                UserId = p.UserId,
                GiftId = p.GiftId,
                Qty = p.Qty,
                Status = p.Status,
                PurchaseDate = p.PurchaseDate
            };
    }
}
