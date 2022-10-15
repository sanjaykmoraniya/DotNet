using Microsoft.AspNetCore.Mvc;
using NZWalkAPISKM.Repositories;
using NZWalkAPISKM.Models.Domain;
using AutoMapper;

namespace NZWalkAPISKM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResionsController : Controller
    {
        private readonly IResionRepository resionRepository;
        private readonly IMapper mapper;

        public ResionsController(IResionRepository resionRepository, IMapper mapper)
        {
            this.resionRepository = resionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResionsAsync()
        {
            var regions = await resionRepository.GetAllAsync();
            var resionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(resionsDTO);
        }
    }
}
