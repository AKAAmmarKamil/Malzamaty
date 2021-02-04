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
    public class SubjectController : BaseController
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetSubjectById")]
        public async Task<ActionResult<SubjectWriteDto>> GetSubjectById(Guid Id)
        {
            var result = await _subjectService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var SubjectModel = _mapper.Map<SubjectWriteDto>(result);
            return Ok(SubjectModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<SubjectReadDto>> GetAllSubjects(int PageNumber, int Count)
        {
            var result = await _subjectService.All(PageNumber, Count);
            var SubjectModel = _mapper.Map<IList<SubjectReadDto>>(result);
            return Ok(SubjectModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] SubjectWriteDto subjectWriteDto)
        {
            var SubjectModel = _mapper.Map<Subject>(subjectWriteDto);
            await _subjectService.Create(SubjectModel);
            var SubjectReadDto = _mapper.Map<SubjectReadDto>(SubjectModel);
            return CreatedAtRoute("GetSubjectById", new { Id = SubjectReadDto.ID }, SubjectReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(Guid Id, [FromBody] SubjectWriteDto subjectWriteDto)
        {
            var SubjectModelFromRepo = await _subjectService.FindById(Id);
            if (SubjectModelFromRepo == null)
            {
                return NotFound();
            }
            var SubjectModel = _mapper.Map<Subject>(subjectWriteDto);
            await _subjectService.Modify(Id, SubjectModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjects(Guid Id)
        {
            var Subject = await _subjectService.Delete(Id);
            if (Subject == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}