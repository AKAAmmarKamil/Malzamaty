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
    public class RatingController : BaseController
    {
        private readonly IRatingService _ratingService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;
        public RatingController(IRatingService ratingService,IFileService fileService,IUserService userService, IMapper mapper)
        {
            _ratingService = ratingService;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetRatingById")]
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
        public async Task<ActionResult<RatingReadDto>> GetAllRatings(int PageNumber,int Count)
        {
            var result =await _ratingService.All(PageNumber,Count);
            var RatingModel = _mapper.Map<IList<RatingReadDto>>(result);
            return Ok(RatingModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RatingReadDto>> AddRating([FromBody] RatingWriteDto RatingWriteDto)
        {
            GetClaim("ID");
            var RatingModel = _mapper.Map<Rating>(RatingWriteDto);
            RatingModel.File =await _fileService.FindById(RatingWriteDto.FileID);
            RatingModel.User = await _userService.FindById(Guid.Parse(GetClaim("ID")));
            await _ratingService.Create(RatingModel);
            var Result = _ratingService.FindById(RatingModel.ID);
            var RatingReadDto = _mapper.Map<RatingReadDto>(Result.Result);
            return CreatedAtRoute("GetRatingById", new { Id = RatingReadDto.Id }, RatingReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(Guid Id, [FromBody] RatingWriteDto RatingWriteDto)
        {
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