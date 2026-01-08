using server.Models;
using server.Models.Enums;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.DTOs;

namespace server.Services.Implementations;

public class GiftService : IGiftService
{
    private readonly IGiftRepository _giftRepository;

    public GiftService(IGiftRepository giftRepository)
    {
        _giftRepository = giftRepository;
    }

    public async Task<IEnumerable<GiftResponseDto>> GetAllGiftsAsync(PriceSort sort)
    {
        var gifts = await _giftRepository.GetAllGiftsAsync(sort);

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryName = g.Category.Name,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId,
            ImageUrl = g.ImageUrl
        });
    }

    public async Task<GiftResponseDto?> GetGiftByIdAsync(int id)
    {
        var gift = await _giftRepository.GetGiftByIdAsync(id);
        if (gift == null)
            return null;

        return new GiftResponseDto
        {
            Id = gift.Id,
            Description = gift.Description,
            CategoryName = gift.Category.Name,
            CategoryId = gift.CategoryId,
            Price = gift.Price,
            DonorId = gift.DonorId,
            ImageUrl = gift.ImageUrl
        };
    }

    public async Task<IEnumerable<GiftResponseDto?>> GetByGiftByCategoryAsync(int categoryId)
    {
        var gifts = await _giftRepository.GetByGiftByCategoryAsync(categoryId);
        if (gifts == null) return null;

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryName = g.Category.Name,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId,
            ImageUrl = g.ImageUrl
        });
    }

    public async Task<GiftResponseDto> AddGiftAsync(GiftCreateWithImageDto dto)
    {
        var imageUrl = string.Empty;
        if (dto.Image != null)
        {
                   // 1. ולידציה בסיסית
        if (dto.Image == null || dto.Image.Length == 0)
            throw new Exception("Image is required");

        // 2. יצירת שם ייחודי
        var extension = Path.GetExtension(dto.Image.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";

        // 3. נתיב פיזי
        var folderPath = Path.Combine("wwwroot", "uploads", "gifts");
        Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, fileName);

        // 4. שמירה בפועל
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.Image.CopyToAsync(stream);
        }

        // 5. יצירת URL
        imageUrl = $"/uploads/gifts/{fileName}";
        }
 

        var model = new GiftModel
        {
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            Price = dto.Price,
            DonorId = dto.DonorId,
            ImageUrl = imageUrl
        };

        var created = await _giftRepository.AddGiftAsync(model);

        return new GiftResponseDto
        {
            Id = created.Id,
            Description = created.Description,
            CategoryName = created.Category.Name,
            Price = created.Price,
            DonorId = created.DonorId,
            ImageUrl = created.ImageUrl
        };
    }


    public async Task<GiftResponseDto> UpdateGiftAsync(int id, GiftUpdateWithImageDto dto)
    {
        var existing = await _giftRepository.GetGiftByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Gift {id} not found");

        if (dto.Description != null)
            existing.Description = dto.Description;

        if (dto.Price.HasValue)
            existing.Price = dto.Price.Value;

        if (dto.CategoryId.HasValue)
            existing.CategoryId = dto.CategoryId.Value;

        if (dto.Image != null)
        {
            // 1. ולידציה בסיסית
            if (dto.Image == null || dto.Image.Length == 0)
                throw new Exception("Image is required");

            // 2. יצירת שם ייחודי
            var extension = Path.GetExtension(dto.Image.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            // 3. נתיב פיזי
            var folderPath = Path.Combine("wwwroot", "uploads", "gifts");
            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            // 4. שמירה בפועל
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            // 5. יצירת URL
            existing.ImageUrl = $"/uploads/gifts/{fileName}";
        } 

        var updated = await _giftRepository.UpdateGiftAsync(existing);

        if (updated == null)
            throw new KeyNotFoundException($"Gift {id} not found");

        return new GiftResponseDto
        {
            Id = updated.Id,
            Description = updated.Description,
            CategoryName = updated.Category.Name,
            CategoryId = updated.CategoryId,
            Price = updated.Price,
            DonorId = updated.DonorId
        };
    }


    public async Task<bool> DeleteGiftAsync(int id)
    {
        if (await _giftRepository.HasPurchasesAsync(id))
            throw new InvalidOperationException($"Cannot delete Gift {id} because it has associated purchases.");
        var result = await _giftRepository.DeleteGiftAsync(id);
        if (!result)
            throw new KeyNotFoundException($"Gift {id} not found");
        return result;
    }

    public async Task<IEnumerable<GiftResponseDto>> FilterByGiftName(string name)
    {
        var gifts = await _giftRepository.FilterByGiftName(name);

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryName = g.Category.Name,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId
        });
    }

    public async Task<IEnumerable<GiftResponseDto>> FilterByGiftDonor(string name)
    {
        var gifts = await _giftRepository.FilterByGiftDonor(name);

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryName = g.Category.Name,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId
        });
    }
    public async Task<IEnumerable<GiftResponseDto>> GetByDonorAsync(int donorId)
    {
        var gifts = await _giftRepository.GetByDonorAsync(donorId);

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryName = g.Category.Name,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId
        });
    }

}