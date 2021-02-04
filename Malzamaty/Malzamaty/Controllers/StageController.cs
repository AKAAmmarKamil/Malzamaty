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
    public class StageController : BaseController
    {
        private readonly IStageService _stageService;
        private readonly IMapper _mapper;
        public StageController(IMapper mapper, IStageService stageService)
        {
            _stageService = stageService;
            _mapper = mapper;
        }

        [HttpGet("{Id}", Name = "GetStageById")]
        public async Task<ActionResult<StageWriteDto>> GetStageById(Guid Id)
        {
            var result = await _stageService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var StageModel = _mapper.Map<StageWriteDto>(result);
            return Ok(StageModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<StageReadDto>> GetAllStages(int PageNumber, int Count)
        {
            var result = await _stageService.All(PageNumber, Count);
            var StageModel = _mapper.Map<IList<StageReadDto>>(result);
            return Ok(StageModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddStage([FromBody] StageWriteDto StageWriteDto)
        {
            var StageModel = _mapper.Map<Stage>(StageWriteDto);
            await _stageService.Create(StageModel);
            var StageReadDto = _mapper.Map<StageReadDto>(StageModel);
            return CreatedAtRoute("GetStageById", new { Id = StageReadDto.ID }, StageReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStage(Guid Id, [FromBody] StageWriteDto StageWriteDto)
        {
            var StageModelFromRepo = await _stageService.FindById(Id);
            if (StageModelFromRepo == null)
            {
                return NotFound();
            }
            var StageModel = _mapper.Map<Stage>(StageWriteDto);
            await _stageService.Modify(Id, StageModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage(Guid Id)
        {
            var Stage = await _stageService.Delete(Id);
            if (Stage == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}