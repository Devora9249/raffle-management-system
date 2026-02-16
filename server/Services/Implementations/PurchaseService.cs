using AutoMapper;
using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repo;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseResponseDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<PurchaseResponseDto>>(
                    await _repo.GetAllAsync()
            );

        }

        public async Task<PurchaseResponseDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : _mapper.Map<PurchaseResponseDto>(p);
        }

        public async Task<PurchaseResponseDto> AddAsync(PurchaseCreateDto createDto)
        {
            if (createDto.Qty <= 0) throw new ArgumentException("Qty must be greater than 0");

            var purchase = _mapper.Map<PurchaseModel>(createDto);


            var created = await _repo.AddAsync(purchase);
            return _mapper.Map<PurchaseResponseDto>(created);
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
            return _mapper.Map<PurchaseResponseDto>(updated);
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
        {
            return _mapper.Map<IEnumerable<PurchaseResponseDto>>(
                await _repo.GetByGiftAsync(giftId)
            );
        }

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

  

    }
}
