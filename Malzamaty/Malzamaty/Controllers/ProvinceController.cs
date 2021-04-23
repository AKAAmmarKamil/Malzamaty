using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    public class ProvinceController : BaseController
    {
        private readonly IProvinceService _ProvinceService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        public ProvinceController(IProvinceService ProvinceService,ICountryService countryService, IMapper mapper)
        {
            _ProvinceService = ProvinceService;
            _countryService = countryService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetProvinceById")]
        public async Task<ActionResult<ProvinceWriteDto>> GetProvinceById(Guid Id)
        {
            var result = await _ProvinceService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ProvinceModel = _mapper.Map<ProvinceWriteDto>(result);
            return Ok(ProvinceModel);
        }
        [HttpGet]
        public async Task<ActionResult<ProvinceReadDto>> GetProvincesByCountry(Guid CountryId)
        {
            var result = await _ProvinceService.GetByCountry(CountryId);
            var ProvinceModel = _mapper.Map<IList<ProvinceReadDto>>(result);
            return Ok(ProvinceModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddProvince([FromBody] ProvinceWriteDto ProvinceWriteDto)
        {
            var ProvinceModel = _mapper.Map<Province>(ProvinceWriteDto);
            var Result=await _ProvinceService.Create(ProvinceModel);
            Result.Country =await _countryService.FindById(Result.CountryID);
            var ProvinceReadDto = _mapper.Map<ProvinceReadDto>(Result);
            return CreatedAtRoute("GetProvinceById", new { Id = ProvinceReadDto.ID }, ProvinceReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvince(Guid Id, [FromBody] ProvinceWriteDto ProvinceWriteDto)
        {
            var ProvinceModelFromRepo = await _ProvinceService.FindById(Id);
            if (ProvinceModelFromRepo == null)
            {
                return NotFound();
            }
            var ProvinceModel = _mapper.Map<Province>(ProvinceWriteDto);
            await _ProvinceService.Modify(Id, ProvinceModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvinces(Guid Id)
        {
            var Province = await _ProvinceService.Delete(Id);
            if (Province == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}