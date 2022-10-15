using NZWalkAPISKM.Models.Domain;

namespace NZWalkAPISKM.Repositories
{
    public interface IResionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
