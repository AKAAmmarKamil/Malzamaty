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
    public class MahallahController : BaseController
    {
        private readonly IDistrictService _districtService;
        private readonly IMahallahService _MahallahService;
        private readonly IMapper _mapper;
        public MahallahController(IMahallahService MahallahService,IDistrictService districtService, IMapper mapper)
        {
            _MahallahService = MahallahService;
            _districtService = districtService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetMahallahById")]
        public async Task<ActionResult<MahallahReadDto>> GetMahallahById(Guid Id)
        {
            var result = await _MahallahService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var MahallahModel = _mapper.Map<MahallahReadDto>(result);
            return Ok(MahallahModel);
        }
        [HttpGet]
        public async Task<ActionResult<MahallahReadDto>> GetMahallahsByDistrict(Guid DistrictId)
        {
            var result = await _MahallahService.GetByDistrict(DistrictId);
            var MahallahModel = _mapper.Map<IList<MahallahReadDto>>(result);
            return Ok(MahallahModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddMahallah([FromBody] MahallahWriteDto MahallahWriteDto)
        {
            var MahallahModel = _mapper.Map<Mahallah>(MahallahWriteDto);
            var Result = await _MahallahService.Create(MahallahModel);
            Result.District = await _districtService.FindById(Result.DistrictID);
            var MahallahReadDto = _mapper.Map<MahallahReadDto>(Result);
            return CreatedAtRoute("GetMahallahById", new { Id = MahallahReadDto.ID }, MahallahReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMahallah(Guid Id, [FromBody] MahallahUpdateDto MahallahUpdateDto)
        {
            var MahallahModelFromRepo = await _MahallahService.FindById(Id);
            if (MahallahModelFromRepo == null)
            {
                return NotFound();
            }
            var MahallahModel = _mapper.Map<Mahallah>(MahallahUpdateDto);
            await _MahallahService.Modify(Id, MahallahModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMahallahs(Guid Id)
        {
            var Mahallah = await _MahallahService.Delete(Id);
            if (Mahallah == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}