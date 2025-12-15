using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Implementations;

public class DonorRepository : IDonorRepository
{
    private readonly AppDbContext _context;

    public DonorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DonorModel>> GetAllDonorsAsync()
        => await _context.Donors.ToListAsync();

    public async Task<DonorModel?> GetDonorByIdAsync(int id)
        => await _context.Donors.FindAsync(id);

    public async Task<DonorModel> AddDonorAsync(DonorModel donor)
    {
        _context.Donors.Add(donor);
        await _context.SaveChangesAsync();
        return donor;
    }

    public async Task<DonorModel> UpdateDonorAsync(DonorModel donor)
    {
        var existing = await _context.Donors.FindAsync(donor.Id);
        if (existing == null) throw new KeyNotFoundException("Donor not found");

        _context.Entry(existing).CurrentValues.SetValues(donor);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteDonorAsync(int id)
    {
        var donor = await _context.Donors.FindAsync(id);
        if (donor == null) return false;

        _context.Donors.Remove(donor);
        await _context.SaveChangesAsync();
        return true;
    }
}
