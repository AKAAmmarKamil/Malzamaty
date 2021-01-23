using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public InterestController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetInterestById")]
        public async Task<ActionResult<InterestReadDto>> GetInterestById(Guid Id)
        {
            var result = await _wrapper.Interest.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var InterestModel = _mapper.Map<InterestReadDto>(result);
            return Ok(InterestModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<InterestReadDto>> GetAllInterests(int PageNumber,int Count)
        {
            var result = await _wrapper.Interest.FindAll(PageNumber,Count);
            var InterestModel = _mapper.Map<IList<InterestReadDto>>(result);
            return Ok(InterestModel);
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Teacher)]
        [HttpPost]
        public async Task<ActionResult<InterestReadDto>> AddInterest([FromBody] InterestWriteDto InterestWriteDto)
        {
            var InterestModel = _mapper.Map<Interests>(InterestWriteDto);
            InterestModel.User =await _wrapper.User.FindById(Guid.Parse(GetClaim("ID")));
            await _wrapper.Interest.Create(InterestModel);
            var Result = _wrapper.Interest.FindById(InterestModel.ID);
            var InterestReadDto = _mapper.Map<InterestReadDto>(Result.Result);
            return CreatedAtRoute("GetInterestById", new { Id = InterestReadDto.ID }, InterestReadDto);
        }
        [Authorize(Roles =UserRole.Admin+","+UserRole.Student)]
        [HttpPut]
        public async Task<IActionResult> UpdateInterestBySchedule()
        {
            var InterestModelFromRepo = await _wrapper.Interest.FindByUser(Guid.Parse(GetClaim("ID")));
            if (InterestModelFromRepo == null)
            {
                return NotFound();
            }
            var Schedules = _wrapper.Schedule.GetUserSchedules(Guid.Parse(GetClaim("ID"))).Result.ToList();
            for (int i = 0; i < Schedules.Count; i++)
            {
                if (await _wrapper.Schedule.IsBewteenTwoDates(DateTime.Now, Schedules[i].StartStudy, Schedules[i].FinishStudy) == false &&
                    await _wrapper.Schedule.IsBewteenTwoDates(DateTime.Now, Schedules[i].StartStudy, Schedules[i].FinishStudy) == false)
                {
                    InterestModelFromRepo.SubjectID = Schedules[i].Subject.ID;
                    _wrapper.Save();
                }
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterest(Guid Id, [FromBody] InterestWriteDto InterestWriteDto)
        {
            var InterestModelFromRepo =await _wrapper.Interest.FindById(Id);
            if (InterestModelFromRepo == null)
            {
                return NotFound();
            }
            InterestModelFromRepo.ClassID = InterestWriteDto.Class;
            InterestModelFromRepo.SubjectID = InterestWriteDto.Subject;
            _wrapper.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterests(Guid Id)
        {
            var CheckIfLast = await _wrapper.Interest.CheckIfLast(Id);
            if (CheckIfLast == false)
                return BadRequest(new { error = "لا يمكن حذف آخر إهتمام" });
            var Interest = _wrapper.Interest.Delete(Id);
            if (Interest.Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}