using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IDonorRepository
    {
        Task<IEnumerable<DonorModel>> GetAllDonorsAsync();
        Task<DonorModel?> GetDonorByIdAsync(int id);
        Task<DonorModel> AddDonorAsync(DonorModel donor);
        Task<DonorModel> UpdateDonorAsync(DonorModel donor);
        Task<bool> DeleteDonorAsync(int id);
    }
}
