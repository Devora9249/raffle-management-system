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
                Status = Status.Completed,
                PurchaseDate = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(purchase);
            return ToResponseDto(created);
        }
        public async Task<PurchaseResponseDto> UpdateAsync(int id, PurchaseUpdateDto dto)
        {
            // שליפת הרכישה
            var purchase = await _repo.GetByIdAsync(id);
            if (purchase == null)
                throw new KeyNotFoundException("Purchase not found");

            // תנאי
            if (dto.Qty.HasValue && dto.Qty.Value < 0)
                throw new ArgumentException("Qty must be greater than 0");

            //  עדכון שדות
            if (dto.Qty.HasValue)
                purchase.Qty = dto.Qty.Value;

            if (dto.Status.HasValue)
                purchase.Status = dto.Status.Value;

            //  שמירה 
            var updated = await _repo.UpdateAsync(purchase);
            if (updated == null)
                throw new KeyNotFoundException("Purchase not found");

            // החזרת DTO
            return ToResponseDto(updated);
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null)
                throw new KeyNotFoundException("Purchase not found");

            var deleted = await _repo.DeleteAsync(id);

            if (!deleted)
                throw new KeyNotFoundException("Purchase not found or could not be deleted");
            return deleted;
        }

        public async Task<IEnumerable<PurchaseResponseDto>> GetByGiftAsync(int giftId)
            => (await _repo.GetByGiftAsync(giftId)).Select(ToResponseDto).ToList();

        public async Task<IEnumerable<GiftPurchaseCountDto>> GetPurchaseCountByGiftAsync()
        {
            var data = await _repo.GetPurchaseCountByGiftAsync();

            return data.Select(x => new GiftPurchaseCountDto
            {
                GiftId = x.GiftId,
                GiftName = x.GiftName,
                PurchaseCount = x.PurchaseCount,
                DonorName = x.DonorName
            });
        }

        private static PurchaseResponseDto ToResponseDto(PurchaseModel p)
        {

            return new PurchaseResponseDto
            {
                Id = p.Id,
                UserId = p.UserId,
                UserName = p.User.Name,
                GiftId = p.GiftId,
                GiftName = p.Gift.Description,
                DonorId = p.Gift.DonorId,
                DonorName = p.Gift.Donor.Name,
                Qty = p.Qty,
                Status = p.Status,
                PurchaseDate = p.PurchaseDate
            };

        }

    }
}
