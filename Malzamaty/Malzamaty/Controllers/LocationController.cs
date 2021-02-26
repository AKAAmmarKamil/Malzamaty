using AutoMapper;
using Malzamaty.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class LocationController : BaseController
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        public LocationController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }
        //AIzaSyDLCxsuLkIU_h_qJHvPemveh_75dyHSNCA
        [HttpGet()]
        public async Task<IActionResult> GetLocationById(string CurrentLat,string Currentlng)
        {
            var address = "Ibadan, Nigeria";

            var CurrentLocation =  System.Data.Entity.Spatial.DbGeography.FromText("POINT( "+CurrentLat+" "+ Currentlng +" )");
         
            return Ok(new { CurrentLocation});
        }
        
    }
}