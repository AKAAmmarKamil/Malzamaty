using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class InterestController : BaseController
    {
        private readonly IInterestService _interestService;
        private readonly IUserService _userService;
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;
        public InterestController(IInterestService interestService, IUserService userService, IScheduleService scheduleService, IMapper mapper)
        {
            _interestService = interestService;
            _userService = userService;
            _scheduleService = scheduleService;
            _mapper = mapper;
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        [HttpGet("{Id}", Name = "GetInterestById")]
        public async Task<ActionResult<InterestReadDto>> GetInterestById(Guid Id)
        {
            var result = await _interestService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var InterestModel = _mapper.Map<InterestReadDto>(result);
            return Ok(InterestModel);
        }
        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<InterestReadDto>> GetAllInterests(int PageNumber, int Count)
        {
            var result = await _interestService.All(PageNumber, Count);
            var InterestModel = _mapper.Map<IList<InterestReadDto>>(result);
            return Ok(InterestModel);
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Teacher)]
        [HttpPost]
        public async Task<ActionResult<InterestReadDto>> AddInterest([FromBody] InterestWriteDto InterestWriteDto)
        {
            var InterestModel = _mapper.Map<Interests>(InterestWriteDto);
            InterestModel.User = await _userService.FindById(Guid.Parse(GetClaim("ID")));
            var Result = await _interestService.Create(InterestModel);
            var InterestReadDto = _mapper.Map<InterestReadDto>(Result);
            return CreatedAtRoute("GetInterestById", new { Id = InterestReadDto.ID }, InterestReadDto);
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student)]
        [HttpPut]
        public async Task<IActionResult> UpdateInterestBySchedule()
        {
            var Now = DateTime.Now;
            var InterestModelFromRepo = await _interestService.FindByUser(Guid.Parse(GetClaim("ID")));
            if (InterestModelFromRepo == null)
            {
                return NotFound();
            }
            var Schedules = _scheduleService.GetUserSchedules(Guid.Parse(GetClaim("ID"))).Result.ToList();
            for (int i = 0; i < Schedules.Count; i++)
            {
                if (await _scheduleService.IsBewteenTwoDates(Now, Schedules[i].StartStudy, Schedules[i].FinishStudy) == true)
                {
                    await _interestService.ModifyBySchedule(Guid.Parse(GetClaim("ID")), Schedules[i].Subject.ID);
                }
            }
            return NoContent();
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterest(Guid Id, [FromBody] InterestWriteDto InterestWriteDto)
        {
            var InterestModelFromRepo = await _interestService.FindById(Id);
            if (InterestModelFromRepo == null)
            {
                return NotFound();
            }
            var InterestModel = _mapper.Map<Interests>(InterestWriteDto);
            await _interestService.Modify(Id, InterestModel);
            return NoContent();
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterests(Guid Id)
        {
            var CheckIfLast = await _interestService.CheckIfLast(Id);
            if (CheckIfLast == false)
                return BadRequest(new { error = "لا يمكن حذف آخر إهتمام" });
            var Interest = _interestService.Delete(Id);
            if (Interest.Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}