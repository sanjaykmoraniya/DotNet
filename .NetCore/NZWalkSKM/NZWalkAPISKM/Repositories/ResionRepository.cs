using Microsoft.EntityFrameworkCore;
using NZWalkAPISKM.Data;
using NZWalkAPISKM.Models.Domain;

namespace NZWalkAPISKM.Repositories
{
    public class ResionRepository : IResionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public ResionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
    }
}
