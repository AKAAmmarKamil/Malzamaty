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
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IStageService _stageService;
        private readonly IClassTypeService _classTypeService;
        private readonly ICountryService _countryService;

        private readonly IMapper _mapper;
        public ClassController(IClassService classService, IStageService stageService, IClassTypeService classTypeService,ICountryService countryService, IMapper mapper)
        {
            _classService = classService;
            _stageService = stageService;
            _classTypeService = classTypeService;
            _countryService = countryService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetClassById")]
        public async Task<ActionResult<ClassReadDto>> GetClassById(Guid Id)
        {
            var result = await _classService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ClassModel = _mapper.Map<ClassReadDto>(result);
            return Ok(ClassModel);
        }
        [HttpGet]
        public async Task<ActionResult<ClassReadDto>> GetAllClasses(Guid Country)
        {
            var result = await _classService.AllInCountry(Country);
            var ClassModel = _mapper.Map<IList<ClassReadDto>>(result);
            return Ok(ClassModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddClass([FromBody] ClassWriteDto ClassWriteDto)
        {
            var ClassModel = _mapper.Map<Class>(ClassWriteDto);
            var Result = await _classService.Create(ClassModel);
            Result.Country =await _countryService.FindById(Result.CountryID);
            Result.Stage = await _stageService.FindById(Result.StageID);
            Result.ClassType = await _classTypeService.FindById(Result.ClassTypeID);
            var ClassReadDto = _mapper.Map<ClassReadDto>(Result);
            return CreatedAtRoute("GetClassById", new { Id = ClassReadDto.ID }, ClassReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid Id, [FromBody] ClassUpdateDto ClassUpdateDto)
        {
            var ClassModelFromRepo = await _classService.FindById(Id);
            if (ClassModelFromRepo == null)
            {
                return NotFound();
            }
            var ClassModel = _mapper.Map<Class>(ClassUpdateDto);
            await _classService.Modify(Id, ClassModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(Guid Id)
        {
            var Class = await _classService.Delete(Id);
            if (Class == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}