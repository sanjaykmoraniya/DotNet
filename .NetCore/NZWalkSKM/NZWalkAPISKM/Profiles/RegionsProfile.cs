using AutoMapper;

namespace NZWalkAPISKM.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            this.CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
        }
    }
}
