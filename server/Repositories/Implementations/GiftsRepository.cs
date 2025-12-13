using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;


namespace server.Repositories.Implementations;


public class GiftsRepository : IGiftsRepository
{
    private readonly AppDbContext _context;

    public GiftsRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<GiftModel>> GetAllGiftsAsync()
    {
        return await _context.Gifts
        .Include(g => g.Category)
        .ToListAsync();
    }

    public async Task<GiftModel?> GetGiftByIdAsync(int id)
    {
        return await _context.Gifts
        .Include(g => g.Category)
        .FirstOrDefaultAsync(g => g.Id == id);
    }


    public async Task<GiftModel> AddGiftAsync(GiftModel gift)
    {
        _context.Gifts.Add(gift);
        await _context.SaveChangesAsync();
        return gift;
    }

    public async Task<GiftModel> UpdateGiftAsync(GiftModel gift)
    {
        var existingGift = await _context.Gifts.FindAsync(gift.Id);
        if (existingGift == null)
        {
            throw new KeyNotFoundException("Gift not found");
        }
        _context.Entry(existingGift).CurrentValues.SetValues(gift);
        await _context.SaveChangesAsync();
        return existingGift;
    }

    public async Task<bool> DeleteGiftAsync(int id)
    {
        var gift = await _context.Gifts.FindAsync(id);
        if (gift == null)
        {
            return false;
        }
        _context.Gifts.Remove(gift);
        await _context.SaveChangesAsync();
        return true;
    }

}



