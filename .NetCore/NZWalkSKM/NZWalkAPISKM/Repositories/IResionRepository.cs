using NZWalkAPISKM.Models.Domain;

namespace NZWalkAPISKM.Repositories
{
    public interface IResionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);
        Task<Region> AddAsync(Region region);
        Task<Region> DeleteAsync(Guid id);
        Task<Region> UpdateAsync(Guid id, Region region);
    }
}
