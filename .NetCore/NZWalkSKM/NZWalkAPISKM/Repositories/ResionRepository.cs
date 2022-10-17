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

        public async Task<Region> GetAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            dbContext.Regions.Add(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await dbContext.Regions.FindAsync(id);
            if (region == null) return null;
            dbContext.Regions.Remove(region);
            dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FindAsync(id);

            if (existingRegion == null) return null;

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
