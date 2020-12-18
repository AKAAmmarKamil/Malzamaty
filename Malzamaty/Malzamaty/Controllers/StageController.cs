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
    public class StageController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public StageController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
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
        public async Task<ActionResult<CountryReadDto>> GetAllStages(int PageNumber, int Count)
        {
            var result = await _wrapper.Country.FindAll(PageNumber, Count);
            var CountryModel = _mapper.Map<IList<CountryReadDto>>(result);
            return Ok(CountryModel);
        }
        [HttpPost]
        public async Task<ActionResult<StageReadDto>> AddStage([FromBody] StageWriteDto StageWriteDto)
        {
            var StageModel = _mapper.Map<Stage>(StageWriteDto);
            await _wrapper.Stage.Create(StageModel);
            var StageReadDto = _mapper.Map<StageReadDto>(StageModel);
            return Ok(StageReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStage(Guid Id, [FromBody] StageWriteDto StageWriteDto)
        {
            var StageModelFromRepo = _wrapper.Stage.FindById(Id);
            if (StageModelFromRepo.Result == null)
            {
                return NotFound();
            }
            StageModelFromRepo.Result.Name = StageWriteDto.Name;
            _wrapper.Stage.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage(Guid Id)
        {
            var Stage = _wrapper.Stage.Delete(Id);
            if (Stage.Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}