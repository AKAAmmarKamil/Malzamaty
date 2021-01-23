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
    public class StageController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public StageController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpGet("{Id}",Name = "GetStageById")]
        public async Task<ActionResult<StageWriteDto>> GetStageById(Guid Id)
        {
            var result = await _wrapper.Stage.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var StageModel = _mapper.Map<StageWriteDto>(result);
            return Ok(StageModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<StageReadDto>> GetAllStages(int PageNumber,int Count)
        {
            var result = await _wrapper.Stage.FindAll(PageNumber,Count);
            var StageModel = _mapper.Map<IList<StageReadDto>>(result);
            return Ok(StageModel);
        }
        [HttpPost]
        public async Task<ActionResult<StageReadDto>> AddStage([FromBody] StageWriteDto StageWriteDto)
        {
            var StageModel = _mapper.Map<Stage>(StageWriteDto);
            await _wrapper.Stage.Create(StageModel);
            var StageReadDto = _mapper.Map<StageReadDto>(StageModel);
            return CreatedAtRoute("GetStageById", new { Id = StageReadDto.ID }, StageReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStage(Guid Id, [FromBody] StageWriteDto StageWriteDto)
        {
            var StageModelFromRepo =await _wrapper.Stage.FindById(Id);
            if (StageModelFromRepo == null)
            {
                return NotFound();
            }
            StageModelFromRepo.Name = StageWriteDto.Name;
            _wrapper.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage(Guid Id)
        {
            var Stage =await _wrapper.Stage.Delete(Id);
            if (Stage == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}