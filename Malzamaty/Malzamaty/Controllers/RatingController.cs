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
    public class RatingController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public RatingController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<RatingReadDto>> GetRatingById(Guid Id)
        {
            var result = await _wrapper.Rating.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var RatingModel = _mapper.Map<RatingReadDto>(result);
            return Ok(RatingModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<RatingReadDto>> GetAllRatings(int PageNumber, int Count)
        {
            var result =_wrapper.Rating.FindAll(PageNumber, Count).Result.ToList();
            var RatingModel = _mapper.Map<List<RatingReadDto>>(result);
            return Ok(RatingModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RatingReadDto>> AddRating([FromBody] RatingWriteDto RatingWriteDto)
        {
            GetClaim("ID");
            var RatingModel = _mapper.Map<Rating>(RatingWriteDto);
            RatingModel.File =await _wrapper.File.FindById(RatingWriteDto.FileID);
            RatingModel.User = await _wrapper.User.FindById(Guid.Parse(GetClaim("ID")));
            await _wrapper.Rating.Create(RatingModel);
            var Result = _wrapper.Rating.FindById(RatingModel.ID);
            var RatingReadDto = _mapper.Map<RatingReadDto>(Result.Result);
            return Ok(RatingReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(Guid Id, [FromBody] RatingWriteDto RatingWriteDto)
        {
            var RatingModelFromRepo = await _wrapper.Rating.FindById(Id);
            if (RatingModelFromRepo == null)
            {
                return NotFound();
            }
            RatingModelFromRepo.Comment = RatingWriteDto.Comment;
            RatingModelFromRepo.Rate = RatingWriteDto.Rate;
            _wrapper.User.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatinges(Guid Id)
        {
            var Rating = await _wrapper.Rating.Delete(Id);
            if (Rating == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}