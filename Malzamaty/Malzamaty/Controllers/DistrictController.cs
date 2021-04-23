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
    public class DistrictController : BaseController
    {
        private readonly IProvinceService _ProvinceService;
        private readonly IDistrictService _DistrictService;
        private readonly IMapper _mapper;
        public DistrictController(IDistrictService DistrictService,IProvinceService ProvinceService, IMapper mapper)
        {
            _DistrictService = DistrictService;
            _ProvinceService = ProvinceService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetDistrictById")]
        public async Task<ActionResult<DistrictReadDto>> GetDistrictById(Guid Id)
        {
            var result = await _DistrictService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var DistrictModel = _mapper.Map<DistrictReadDto>(result);
            return Ok(DistrictModel);
        }
        [HttpGet]
        public async Task<ActionResult<DistrictReadDto>> GetDistrictsByProvince(Guid ProvinceId)
        {
            var result = await _DistrictService.GetByProvince(ProvinceId);
            var DistrictModel = _mapper.Map<IList<DistrictReadDto>>(result);
            return Ok(DistrictModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddDistrict([FromBody] DistrictWriteDto DistrictWriteDto)
        {
            var DistrictModel = _mapper.Map<District>(DistrictWriteDto);
            var Result = await _DistrictService.Create(DistrictModel);
            Result.Province =await _ProvinceService.FindById(Result.ProvinceID);
            var DistrictReadDto = _mapper.Map<DistrictReadDto>(Result);
            return CreatedAtRoute("GetDistrictById", new { Id = DistrictReadDto.ID }, DistrictReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistrict(Guid Id, [FromBody] DistrictUpdateDto DistrictUpdateDto)
        {
            var DistrictModelFromRepo = await _DistrictService.FindById(Id);
            if (DistrictModelFromRepo == null)
            {
                return NotFound();
            }
            var DistrictModel = _mapper.Map<District>(DistrictUpdateDto);
            await _DistrictService.Modify(Id, DistrictModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistricts(Guid Id)
        {
            var District = await _DistrictService.Delete(Id);
            if (District == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}