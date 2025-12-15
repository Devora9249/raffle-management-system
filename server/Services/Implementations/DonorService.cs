using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _repo;

        public DonorService(IDonorRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<DonorModel>> GetAllDonorsAsync() => _repo.GetAllDonorsAsync();
        public Task<DonorModel?> GetDonorByIdAsync(int id) => _repo.GetDonorByIdAsync(id);
        public Task<DonorModel> AddDonorAsync(DonorModel donor) => _repo.AddDonorAsync(donor);
        public Task<DonorModel> UpdateDonorAsync(DonorModel donor) => _repo.UpdateDonorAsync(donor);
        public Task<bool> DeleteDonorAsync(int id) => _repo.DeleteDonorAsync(id);
    }
}
