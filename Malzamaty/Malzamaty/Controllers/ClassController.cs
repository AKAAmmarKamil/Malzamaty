using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ClassController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public ClassController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetClassById")]
        public async Task<ActionResult<ClassReadDto>> GetClassById(Guid Id)
        {
            var result = await _wrapper.Class.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ClassModel = _mapper.Map<ClassReadDto>(result);
            return Ok(ClassModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ClassReadDto>> GetAllClasses(int PageNumber, int Count)
        {
            var result = await _wrapper.Class.FindAll(PageNumber, Count);
            var ClassModel = _mapper.Map<IList<ClassReadDto>>(result);
            return Ok(ClassModel);
        }
        [HttpPost]
        public async Task<ActionResult<ClassReadDto>> AddClass([FromBody] ClassWriteDto ClassWriteDto)
        {
            var ClassModel = _mapper.Map<Class>(ClassWriteDto);
            await _wrapper.Class.Create(ClassModel);
            var Result = _wrapper.Class.FindById(ClassModel.ID);
            var ClassReadDto = _mapper.Map<ClassReadDto>(Result.Result);
            return CreatedAtRoute("GetClassById", new { Id = ClassReadDto.ID }, ClassReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid Id, [FromBody] ClassWriteDto ClassWriteDto)
        {
            var ClassModelFromRepo =await _wrapper.Class.FindById(Id);
            if (ClassModelFromRepo == null)
            {
                return NotFound();
            }
            ClassModelFromRepo.Name = ClassWriteDto.Name;
            _wrapper.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(Guid Id)
        {
            var Class =await _wrapper.Class.Delete(Id);
            if (Class == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}