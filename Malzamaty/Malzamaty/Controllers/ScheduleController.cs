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
    [ApiController]
    public class ScheduleController : BaseController
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMatchService _matchService;
        private readonly IInterestService _interestService;
        private readonly IUserService _userService;
        private readonly ISubjectService _subjectService;

        private readonly IMapper _mapper;
        public ScheduleController(IScheduleService scheduleService,IMatchService matchService,IUserService userService,
            IInterestService interestService,ISubjectService subjectService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _matchService = matchService;
            _interestService = interestService;
            _userService = userService;
            _subjectService = subjectService;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetScheduleById")]
        public async Task<ActionResult<ScheduleReadDto>> GetScheduleById(Guid Id)
        {
            var result = await _scheduleService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ScheduleModel = _mapper.Map<ScheduleReadDto>(result);
            return Ok(ScheduleModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ScheduleReadDto>> GetAllSchedules(int PageNumber,int Count)
        {
            var result =await _scheduleService.All(PageNumber,Count);
            var ScheduleModel = _mapper.Map<IList<ScheduleReadDto>>(result);
            return Ok(ScheduleModel);
        }
        [Authorize]
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<ScheduleReadDto>> GetUserSchedules(int PageNumber, int Count)
        {
            var result =await _scheduleService.GetUserSchedules(PageNumber, Count, Guid.Parse(GetClaim("ID")));
            var ScheduleModel = _mapper.Map<IList<ScheduleReadDto>>(result);
            return Ok(ScheduleModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ScheduleReadDto>> AddSchedule([FromBody] ScheduleWriteDto ScheduleWriteDto)
        {
            var Schedules = _scheduleService.GetUserSchedules(Guid.Parse(GetClaim("ID"))).Result.ToList();
            var UserClass = _interestService.FindByUser(Guid.Parse(GetClaim("ID")));
            var Match =await _matchService.FindByClass(UserClass.Result.ClassID,ScheduleWriteDto.Subject);
            if (Match.Count()==0) 
            {
                return BadRequest(new { Error = "المادة غير متوافقة مع صفك" });
            }
            for (int i = 0; i < Schedules.Count; i++)
            {
                if (await _scheduleService.IsBewteenTwoDates(ScheduleWriteDto.StartStudy,Schedules[i].StartStudy,Schedules[i].FinishStudy)==true||
                    await _scheduleService.IsBewteenTwoDates(ScheduleWriteDto.FinishStudy, Schedules[i].StartStudy, Schedules[i].FinishStudy) == true)
                {
                    return BadRequest(new {Error="التاريخ محجوز لدراسة مادة أخرى" });
                }
            }
            var ScheduleModel = _mapper.Map<Schedule>(ScheduleWriteDto);
            ScheduleModel.User = await _userService.FindById(Guid.Parse(GetClaim("ID")));
            ScheduleModel.Subject = await _subjectService.FindById(ScheduleWriteDto.Subject);
            await _scheduleService.Create(ScheduleModel);
            var Result = _scheduleService.FindById(ScheduleModel.ID);
            var ScheduleReadDto = _mapper.Map<ScheduleReadDto>(Result.Result);
            return CreatedAtRoute("GetScheduleById", new { Id = ScheduleReadDto.Id }, ScheduleReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(Guid Id, [FromBody] ScheduleWriteDto ScheduleWriteDto)
        {
            var ScheduleModelFromRepo = await _scheduleService.FindById(Id);
            if (ScheduleModelFromRepo == null)
            {
                return NotFound();
            }
            var ScheduleModel = _mapper.Map<Schedule>(ScheduleWriteDto);
            await _scheduleService.Modify(Id,ScheduleModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedules(Guid Id)
        {
            var Schedule = await _scheduleService.Delete(Id);
            if (Schedule == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}