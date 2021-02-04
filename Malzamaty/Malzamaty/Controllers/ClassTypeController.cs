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
    public class ClassTypeController : BaseController
    {
        private readonly IClassTypeService _classTypeService;
        private readonly IMapper _mapper;
        public ClassTypeController(IClassTypeService classTypeService, IMapper mapper)
        {
            _classTypeService = classTypeService;
            _mapper = mapper;
        }

        [HttpGet("{Id}", Name = "GetClassTypeById")]
        public async Task<ActionResult<ClassTypeWriteDto>> GetClassTypeById(Guid Id)
        {
            var result = await _classTypeService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ClassTypeModel = _mapper.Map<ClassTypeWriteDto>(result);
            return Ok(ClassTypeModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ClassTypeReadDto>> GetAllClassTypes(int PageNumber, int Count)
        {
            var result = await _classTypeService.All(PageNumber, Count);
            var ClassTypesModel = _mapper.Map<IList<ClassTypeReadDto>>(result);
            return Ok(ClassTypesModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddClassType([FromBody] ClassTypeWriteDto ClassTypeWriteDto)
        {
            var ClassTypeModel = _mapper.Map<ClassType>(ClassTypeWriteDto);
            await _classTypeService.Create(ClassTypeModel);
            var ClassTypeReadDto = _mapper.Map<ClassTypeReadDto>(ClassTypeModel);
            return CreatedAtRoute("GetClassTypeById", new { Id = ClassTypeReadDto.ID }, ClassTypeReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassType(Guid Id, [FromBody] ClassTypeWriteDto ClassTypeWriteDto)
        {
            var ClassTypeModelFromRepo = await _classTypeService.FindById(Id);
            if (ClassTypeModelFromRepo == null)
            {
                return NotFound();
            }
            var ClassTypeModel = _mapper.Map<ClassType>(ClassTypeWriteDto);
            await _classTypeService.Modify(Id, ClassTypeModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassType(Guid Id)
        {
            var ClassType = await _classTypeService.Delete(Id);
            if (ClassType == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}