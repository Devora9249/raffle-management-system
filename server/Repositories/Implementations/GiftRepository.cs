// using Microsoft.EntityFrameworkCore;
// using server.Data;
// using server.Models;
// using server.Models.Enums;
// using server.Repositories.Interfaces;


// namespace server.Repositories.Implementations;


// public class GiftRepository : IGiftRepository
// {
//     private readonly AppDbContext _context;

//     public GiftRepository(AppDbContext context)
//     {
//         _context = context;
//     }


//     public async Task<IEnumerable<GiftModel>> GetAllGiftsAsync(PriceSort sort)
//     {
//         IQueryable<GiftModel> query = _context.Gifts.Include(g => g.Category);
        
//         //מיון לפי מחיר
//         switch (sort)
//         {
//             case PriceSort.Ascending:
//                 query = query.OrderBy(g => g.Price);
//                 break;
//             case PriceSort.Descending:
//                 query = query.OrderByDescending(g => g.Price);
//                 break;
//             default:
//                 break;
//         }
//         return await query.ToListAsync();

 
//     }

//     public async Task<GiftModel?> GetGiftByIdAsync(int id)
//     {
//         return await _context.Gifts
//         .Include(g => g.Category)
//         .FirstOrDefaultAsync(g => g.Id == id);
//     }


//     public async Task<GiftModel> AddGiftAsync(GiftModel gift)
//     {
//         _context.Gifts.Add(gift);
//         await _context.SaveChangesAsync();
//         return gift;
//     }

//     public async Task<GiftModel> UpdateGiftAsync(GiftModel gift)
//     {
//         var existingGift = await _context.Gifts.FindAsync(gift.Id);
//         if (existingGift == null)
//         {
//             throw new KeyNotFoundException("Gift not found");
//         }
//         _context.Entry(existingGift).CurrentValues.SetValues(gift);
//         await _context.SaveChangesAsync();
//         return existingGift;
//     }

//     public async Task<bool> DeleteGiftAsync(int id)
//     {
//         var gift = await _context.Gifts.FindAsync(id);
//         if (gift == null)
//         {
//             return false;
//         }
//         _context.Gifts.Remove(gift);
//         await _context.SaveChangesAsync();
//         return true;
//     }

//     public async Task<IEnumerable<GiftModel>> FilterByGiftName(string name)
//     {
//         return await _context.Gifts
//         .Where(g => g.Description.Contains(name))
//         .Include(g => g.Category)
//         .ToListAsync();
//     }

//     public async Task<IEnumerable<GiftModel>> FilterByGiftDonor(string name)
//     {
//         return await _context.Gifts
//         .Include(g => g.Donor)
//         .Include(g => g.Category)
//         .Where(g => g.Donor.Name.Contains(name))
//         .ToListAsync();
//     }


// }
// using Microsoft.EntityFrameworkCore;
// using server.Data;
// using server.Models;
// using server.Repositories.Interfaces;

// namespace server.Repositories.Implementations;

// public class GiftRepository : IGiftRepository
// {
//     private readonly AppDbContext _context;

//     public GiftRepository(AppDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<IEnumerable<GiftModel>> GetAllGiftsAsync()
//         => await _context.Gifts
//             .Include(g => g.Category) // אם יש ניווטים
//             .Include(g => g.Donor)
//             .ToListAsync();

//     public async Task<GiftModel?> GetGiftByIdAsync(int id)
//         => await _context.Gifts
//             .Include(g => g.Category)
//             .Include(g => g.Donor)
//             .FirstOrDefaultAsync(g => g.Id == id);

//     public async Task<GiftModel> AddGiftAsync(GiftModel gift)
//     {
//         _context.Gifts.Add(gift);
//         await _context.SaveChangesAsync();
//         return gift;
//     }

//     public async Task<GiftModel> UpdateGiftAsync(GiftModel gift)
//     {
//         var existing = await _context.Gifts.FindAsync(gift.Id);
//         if (existing == null) throw new KeyNotFoundException("Gift not found");

//         _context.Entry(existing).CurrentValues.SetValues(gift);
//         await _context.SaveChangesAsync();
//         return existing;
//     }

//     public async Task<bool> DeleteGiftAsync(int id)
//     {
//         var gift = await _context.Gifts.FindAsync(id);
//         if (gift == null) return false;

//         _context.Gifts.Remove(gift);
//         await _context.SaveChangesAsync();
//         return true;
//     }

//     public async Task<IEnumerable<GiftModel>> SearchByNameAsync(string name)
//         => await _context.Gifts.Where(g => g.Name.Contains(name)).ToListAsync();

//     public async Task<IEnumerable<GiftModel>> FilterByDonorAsync(int donorId)
//         => await _context.Gifts.Where(g => g.DonorId == donorId).ToListAsync();

//     public async Task<IEnumerable<GiftModel>> SortByPriceAsync(bool ascending)
//         => ascending
//             ? await _context.Gifts.OrderBy(g => g.Price).ToListAsync()
//             : await _context.Gifts.OrderByDescending(g => g.Price).ToListAsync();
// }



