using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        public CountryController(IMapper mapper,ICountryService countryService)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet("{Id}",Name = "GetCountryById")]
        public async Task<ActionResult<CountryWriteDto>> GetCountryById(Guid Id)
        {
            var result = await _countryService.FindById(Id);
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
            var result = await _countryService.All(PageNumber, Count);
            var CountryModel = _mapper.Map<IList<CountryReadDto>>(result);
            return Ok(CountryModel);
        }
        [HttpPost]
        public async Task<ActionResult<CountryReadDto>> AddCountry([FromBody] CountryWriteDto countryWriteDto)
        {
            var CountryModel = _mapper.Map<Country>(countryWriteDto);
            await _countryService.Create(CountryModel);
            var CountryReadDto = _mapper.Map<CountryReadDto>(CountryModel);
            return CreatedAtRoute("GetCountryById", new { Id = CountryReadDto.ID }, CountryReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(Guid Id, [FromBody] CountryWriteDto countryWriteDto)
        {
            var CountryModelFromRepo =await _countryService.FindById(Id);
            if (CountryModelFromRepo == null)
            {
                return NotFound();
            }
            var CountryModel = _mapper.Map<Country>(countryWriteDto);
            await _countryService.Modify(Id, CountryModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid Id)
        {
            var Country =await _countryService.Delete(Id);
            if (Country == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}