using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public CountryController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<IActionResult> GetAllCountries(int PageNumber,int Count)
        {
            var result = await _wrapper.Country.FindAll(PageNumber,Count);
            return Ok(result);
        }/*
        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody]Country country)
        {
            Country Cou = new Country
            {
                Co_ID = Guid.NewGuid().ToString(),
                Co_Name = country.Co_Name,
            };
            _dbContext.Country.Add(Cou);
            await _dbContext.SaveChangesAsync();
            return Ok(country);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody]Country country, string Co_ID)
        {
            var Cou = _dbContext.Class.Find(Co_ID);
            Cou.C_Name = country.Co_Name;
            _dbContext.Entry(Cou).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCountry(string Co_ID)
        {
            var Country = new Country { Co_ID = Co_ID };
            _dbContext.Entry(Country).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }*/
    }
}