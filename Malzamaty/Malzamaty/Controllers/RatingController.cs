﻿using AutoMapper;
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
    [ApiController]
    public class RatingController : BaseController
    {
        private readonly IRatingService _ratingService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;
        public RatingController(IRatingService ratingService, IFileService fileService, IUserService userService, IMapper mapper)
        {
            _ratingService = ratingService;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetRatingById")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<RatingReadDto>> GetRatingById(Guid Id)
        {
            var result = await _ratingService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var RatingModel = _mapper.Map<RatingReadDto>(result);
            return Ok(RatingModel);
        }
        [HttpGet("{Id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<RatingReadDto>> GetRatingByFile(Guid Id)
        {
            var result = await _ratingService.GetRatingByFile(Id);
            if (result == null)
            {
                return NotFound();
            }
            var RatingModel = _mapper.Map<List<RatingReadDto>>(result);
            return Ok(RatingModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<RatingReadDto>> GetAllRatings(int PageNumber, int Count)
        {
            var result = await _ratingService.All(PageNumber, Count);
            var RatingModel = _mapper.Map<IList<RatingReadDto>>(result);
            return Ok(RatingModel);
        }
        [HttpPost]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> AddRating([FromBody] RatingWriteDto RatingWriteDto)
        {
            GetClaim("ID");
            var RatingModel = _mapper.Map<Rating>(RatingWriteDto);
            RatingModel.File = await _fileService.FindById(RatingWriteDto.FileID);
            RatingModel.User = await _userService.FindById(Guid.Parse(GetClaim("ID")));
            var Result = await _ratingService.Create(RatingModel);
            var RatingReadDto = _mapper.Map<RatingReadDto>(Result);
            return CreatedAtRoute("GetRatingById", new { Id = RatingReadDto.Id }, RatingReadDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> UpdateRating(Guid Id, [FromBody] RatingWriteDto RatingWriteDto)
        {
            if (GetClaim("Role") != "Admin" && GetClaim("ID") != Id.ToString())
            {
                return BadRequest(new { Error = "لا يمكن تعديل بيانات تخص مستخدم آخر من دون صلاحية المدير" });
            }
            var RatingModelFromRepo = await _ratingService.FindById(Id);
            if (RatingModelFromRepo == null)
            {
                return NotFound();
            }
            var RatingModel = _mapper.Map<Rating>(RatingWriteDto);
            await _ratingService.Modify(Id, RatingModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> DeleteRatinges(Guid Id)
        {
            var Rating = await _ratingService.Delete(Id);
            if (Rating == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}