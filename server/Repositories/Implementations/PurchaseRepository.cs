using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Implementations;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly AppDbContext _context;

    public PurchaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PurchaseModel>> GetAllAsync()
        => await _context.Purchases
            .Include(p => p.Gift)
            .ToListAsync();

    public async Task<PurchaseModel?> GetByIdAsync(int id)
        => await _context.Purchases
            .Include(p => p.Gift)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<PurchaseModel> AddAsync(PurchaseModel purchase)
    {
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();
        return purchase;
    }

    public async Task<PurchaseModel?> UpdateAsync(PurchaseModel purchase)
    {
        var existing = await _context.Purchases.FindAsync(purchase.Id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(purchase);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Purchases.FindAsync(id);
        if (existing == null) return false;

        _context.Purchases.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<PurchaseModel>> GetByGiftAsync(int giftId)
        => await _context.Purchases
            .Include(p => p.Gift)
            .Where(p => p.GiftId == giftId && p.Status == Status.Completed)
            .ToListAsync();

    public async Task<List<PurchaseModel>> GetUserCartAsync(int userId)
        => await _context.Purchases
            .Include(p => p.Gift)
            .Where(p => p.UserId == userId && p.Status == Status.Draft)
            .ToListAsync();

    public async Task<int> CheckoutAsync(int userId)
    {
        var cartItems = await _context.Purchases
            .Where(p => p.UserId == userId && p.Status == Status.Draft)
            .ToListAsync();

        if (!cartItems.Any())
            return 0;

        foreach (var item in cartItems)
        {
            item.Status = Status.Completed;
            item.PurchaseDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return cartItems.Count;
    }
}
