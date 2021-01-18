using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class MatchController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public MatchController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<MatchReadDto>> GetMatchById(Guid Id)
        {
            var result = await _wrapper.Match.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var MatchModel = _mapper.Map<MatchReadDto>(result);
            return Ok(MatchModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<MatchReadDto>> GetAllMatches(int PageNumber, int Count)
        {
            var result = _wrapper.Match.FindAll(PageNumber, Count).Result.ToList();
            var MatchModel = _mapper.Map<IList<MatchReadDto>>(result);
            return Ok(MatchModel);
        }
        [HttpPost]
        public async Task<ActionResult<MatchReadDto>> AddMatch([FromBody] MatchWriteDto MatchWriteDto)
        {
            var MatchModel = _mapper.Map<Match>(MatchWriteDto);
            await _wrapper.Match.Create(MatchModel);
            var Result = _wrapper.Match.FindById(MatchModel.ID);
            var MatchReadDto = _mapper.Map<MatchReadDto>(Result.Result);
            return Ok(MatchReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(Guid Id, [FromBody] MatchWriteDto MatchWriteDto)
        {
            var MatchModelFromRepo = await _wrapper.Match.FindById(Id);
            if (MatchModelFromRepo == null)
            {
                return NotFound();
            }
            MatchModelFromRepo.C_ID = MatchWriteDto.Class;
            MatchModelFromRepo.Su_ID = MatchWriteDto.Subject;
            _wrapper.User.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatches(Guid Id)
        {
            var Match =await _wrapper.Match.Delete(Id);
            if (Match == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}