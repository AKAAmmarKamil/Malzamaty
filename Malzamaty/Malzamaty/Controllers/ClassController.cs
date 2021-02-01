using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
    [ApiController]
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IMapper _mapper;
        public ClassController(IClassService classService, IMapper mapper)
        {
            _classService = classService;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetClassById")]
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
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ClassReadDto>> GetAllClasses(int PageNumber,int Count)
        {
            var result = await _classService.All(PageNumber,Count);
            var ClassModel = _mapper.Map<IList<ClassReadDto>>(result);
            return Ok(ClassModel);
        }
        [HttpPost]
        public async Task<ActionResult<ClassReadDto>> AddClass([FromBody] ClassWriteDto ClassWriteDto)
        {
            var ClassModel = _mapper.Map<Class>(ClassWriteDto);
            var Result = await _classService.Create(ClassModel);
            var ClassReadDto = _mapper.Map<ClassReadDto>(Result);
            return CreatedAtRoute("GetClassById", new { Id = ClassReadDto.ID }, ClassReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid Id, [FromBody] ClassUpdateDto ClassUpdateDto)
        {
            var ClassModelFromRepo =await _classService.FindById(Id);
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
            var Class =await _classService.Delete(Id);
            if (Class == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}