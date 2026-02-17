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
        private readonly ILogger<PurchaseService> _logger;
        public PurchaseService(IPurchaseRepository repo, IMapper mapper, ILogger<PurchaseService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
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

        public async Task<PurchaseResponseDto> AddAsync(int userId, PurchaseCreateDto createDto)
        {
            _logger.LogInformation("Starting process to add a new purchase for User ID: {UserId}, Gift ID: {GiftId}", userId, createDto.GiftId);
            if (createDto.Qty <= 0)
            {
                _logger.LogWarning("Purchase attempt failed: Qty must be greater than 0. Provided Qty: {Qty}", createDto.Qty);
                throw new ArgumentException("Qty must be greater than 0");
            }

            try
    {
        var purchase = _mapper.Map<PurchaseModel>(createDto);
        var created = await _repo.AddAsync(purchase);
        
        _logger.LogInformation("Purchase successfully created. Purchase ID: {PurchaseId} for User: {UserId}", created.Id, created.UserId);
        
        return _mapper.Map<PurchaseResponseDto>(created);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred while adding purchase for User ID: {UserId}", userId);
        throw;
    }
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
