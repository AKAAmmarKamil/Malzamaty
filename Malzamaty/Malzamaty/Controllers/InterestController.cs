﻿using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
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
    public class InterestController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public InterestController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
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
        public async Task<ActionResult<InterestReadDto>> GetAllInterests(int PageNumber, int Count)
        {
            var result = await _wrapper.Interest.GetAll(PageNumber, Count);
            var InterestModel = _mapper.Map<IList<InterestReadDto>>(result);
            return Ok(InterestModel);
        }
        [HttpPost]
        public async Task<ActionResult<InterestReadDto>> AddInterest([FromBody] InterestWriteDto InterestWriteDto)
        {
            var InterestModel = _mapper.Map<Interests>(InterestWriteDto);
            await _wrapper.Interest.Create(InterestModel);
            var Result = _wrapper.Interest.FindById(InterestModel.ID);
            var InterestReadDto = _mapper.Map<InterestReadDto>(Result.Result);
            return Ok(InterestReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterest(Guid Id, [FromBody] InterestWriteDto InterestWriteDto)
        {
            var InterestModelFromRepo = _wrapper.Interest.FindById(Id);
            if (InterestModelFromRepo.Result == null)
            {
                return NotFound();
            }
            InterestModelFromRepo.Result.C_ID = InterestWriteDto.Class;
            InterestModelFromRepo.Result.Su_ID = InterestWriteDto.Subject;
            _wrapper.User.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterests(Guid Id)
        {
            var CheckIfLast = await _wrapper.Interest.CheckIfLast(Id);
            if (CheckIfLast == false)
                return BadRequest();
            var Interest = _wrapper.Interest.Delete(Id);
            if (Interest.Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}