using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ScheduleController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public ScheduleController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetScheduleById")]
        public async Task<ActionResult<ScheduleReadDto>> GetScheduleById(Guid Id)
        {
            var result = await _wrapper.Schedule.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ScheduleModel = _mapper.Map<ScheduleReadDto>(result);
            return Ok(ScheduleModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ScheduleReadDto>> GetAllSchedules(int PageNumber, int Count)
        {
            var result =_wrapper.Schedule.FindAll(PageNumber, Count).Result.ToList();
            var ScheduleModel = _mapper.Map<List<ScheduleReadDto>>(result);
            return Ok(ScheduleModel);
        }
        [Authorize]
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ScheduleReadDto>> GetUserSchedules(int PageNumber, int Count)
        {
            var result = _wrapper.Schedule.GetUserSchedules(PageNumber, Count, Guid.Parse(GetClaim("ID"))).Result.ToList();
            var ScheduleModel = _mapper.Map<List<ScheduleReadDto>>(result);
            return Ok(ScheduleModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ScheduleReadDto>> AddSchedule([FromBody] ScheduleWriteDto ScheduleWriteDto)
        {
            var Schedules = _wrapper.Schedule.GetUserSchedules(Guid.Parse(GetClaim("ID"))).Result.ToList();
            var UserClass = _wrapper.Interest.FindByUser(Guid.Parse(GetClaim("ID")));
            var Match =await _wrapper.Match.FindByClass(UserClass.Result.ClassID,ScheduleWriteDto.Subject);
            if (Match.Count()==0) 
            {
                return BadRequest(new { Error = "المادة غير متوافقة مع صفك" });
            }
            for (int i = 0; i < Schedules.Count; i++)
            {
                if (await _wrapper.Schedule.IsBewteenTwoDates(ScheduleWriteDto.StartStudy,Schedules[i].StartStudy,Schedules[i].FinishStudy)==true||
                    await _wrapper.Schedule.IsBewteenTwoDates(ScheduleWriteDto.FinishStudy, Schedules[i].StartStudy, Schedules[i].FinishStudy) == true)
                {
                    return BadRequest(new {Error="التاريخ محجوز لدراسة مادة أخرى" });
                }
            }
            var ScheduleModel = _mapper.Map<Schedule>(ScheduleWriteDto);
            ScheduleModel.User = await _wrapper.User.FindById(Guid.Parse(GetClaim("ID")));
            ScheduleModel.Subject = await _wrapper.Subject.FindById(ScheduleWriteDto.Subject);
            await _wrapper.Schedule.Create(ScheduleModel);
            var Result = _wrapper.Schedule.FindById(ScheduleModel.ID);
            var ScheduleReadDto = _mapper.Map<ScheduleReadDto>(Result.Result);
            return CreatedAtRoute("GetScheduleById", new { Id = ScheduleReadDto.Id }, ScheduleReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(Guid Id, [FromBody] ScheduleWriteDto ScheduleWriteDto)
        {
            var ScheduleModelFromRepo = await _wrapper.Schedule.FindById(Id);
            if (ScheduleModelFromRepo == null)
            {
                return NotFound();
            }
            ScheduleModelFromRepo.StartStudy = ScheduleWriteDto.StartStudy;
            ScheduleModelFromRepo.FinishStudy = ScheduleWriteDto.FinishStudy;
            ScheduleModelFromRepo.Subject =await _wrapper.Subject.FindById(ScheduleWriteDto.Subject);
            _wrapper.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedulees(Guid Id)
        {
            var Schedule = await _wrapper.Schedule.Delete(Id);
            if (Schedule == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}