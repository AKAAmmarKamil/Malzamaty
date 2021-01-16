using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("{Id}")]
        public async Task<ActionResult<CountryWriteDto>> GetCountryById(Guid Id)
        {
            var result = await _wrapper.Country.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var CountryModel = _mapper.Map<CountryWriteDto>(result);
            return Ok(CountryModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<CountryReadDto>> GetAllCountries(int PageNumber, int Count)
        {
            var result = await _wrapper.Country.FindAll(PageNumber, Count);
            var CountryModel = _mapper.Map<IList<CountryReadDto>>(result);
            return Ok(CountryModel);
        }
        [HttpPost]
        public async Task<ActionResult<CountryReadDto>> AddCountry([FromBody] CountryWriteDto countryWriteDto)
        {
            var CountryModel = _mapper.Map<Country>(countryWriteDto);
            await _wrapper.Country.Create(CountryModel);
            var CountryReadDto = _mapper.Map<CountryReadDto>(CountryModel);
            return Ok(CountryReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(Guid Id, [FromBody] CountryWriteDto countryWriteDto)
        {
            var CountryModelFromRepo =await _wrapper.Country.FindById(Id);
            if (CountryModelFromRepo == null)
            {
                return NotFound();
            }
            CountryModelFromRepo.Name = countryWriteDto.Name;
            _wrapper.User.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid Id)
        {
            var Country =await _wrapper.Country.Delete(Id);
            if (Country == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}