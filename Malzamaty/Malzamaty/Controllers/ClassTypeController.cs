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
    public class ClassTypeController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public ClassTypeController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpGet("{Id}",Name = "GetClassTypeById")]
        public async Task<ActionResult<ClassTypeWriteDto>> GetClassTypeById(Guid Id)
        {
            var result = await _wrapper.ClassType.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ClassTypeModel = _mapper.Map<ClassTypeWriteDto>(result);
            return Ok(ClassTypeModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ClassTypeReadDto>> GetAllClassTypes(int PageNumber,int Count)
        {
            var result = await _wrapper.ClassType.FindAll(PageNumber,Count);
            var ClassTypesModel = _mapper.Map<IList<ClassTypeReadDto>>(result);
            return Ok(ClassTypesModel);
        }
        [HttpPost]
        public async Task<ActionResult<ClassTypeReadDto>> AddClassType([FromBody] ClassTypeWriteDto ClassTypeWriteDto)
        {
            var ClassTypeModel = _mapper.Map<ClassType>(ClassTypeWriteDto);
            await _wrapper.ClassType.Create(ClassTypeModel);
            var ClassTypeReadDto = _mapper.Map<ClassTypeReadDto>(ClassTypeModel);
            return CreatedAtRoute("GetClassTypeById", new { Id = ClassTypeReadDto.ID }, ClassTypeReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassType(Guid Id, [FromBody] ClassTypeWriteDto ClassTypeWriteDto)
        {
            var ClassTypeModelFromRepo =await _wrapper.ClassType.FindById(Id);
            if (ClassTypeModelFromRepo == null)
            {
                return NotFound();
            }
            ClassTypeModelFromRepo.Name = ClassTypeWriteDto.Name;
            _wrapper.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassType(Guid Id)
        {
            var ClassType =await _wrapper.ClassType.Delete(Id);
            if (ClassType == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}