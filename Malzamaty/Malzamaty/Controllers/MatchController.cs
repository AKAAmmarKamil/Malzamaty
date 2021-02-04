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
    public class MatchController : BaseController
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;
        public MatchController(IMatchService matchService, IMapper mapper)
        {
            _matchService = matchService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetMatchById")]
        public async Task<ActionResult<MatchReadDto>> GetMatchById(Guid Id)
        {
            var result = await _matchService.FindById(Id);
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
            var result = await _matchService.All(PageNumber, Count);
            var MatchModel = _mapper.Map<IList<MatchReadDto>>(result);
            return Ok(MatchModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddMatch([FromBody] MatchWriteDto MatchWriteDto)
        {
            var MatchModel = _mapper.Map<Match>(MatchWriteDto);
            var Result = await _matchService.Create(MatchModel);
            var MatchReadDto = _mapper.Map<MatchReadDto>(Result);
            return CreatedAtRoute("GetMatchById", new { Id = MatchReadDto.ID }, MatchReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(Guid Id, [FromBody] MatchWriteDto MatchWriteDto)
        {
            var MatchModelFromRepo = await _matchService.FindById(Id);
            if (MatchModelFromRepo == null)
            {
                return NotFound();
            }
            var MatchModel = _mapper.Map<Match>(MatchWriteDto);
            await _matchService.Modify(Id, MatchModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatches(Guid Id)
        {
            var Match = await _matchService.Delete(Id);
            if (Match == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}