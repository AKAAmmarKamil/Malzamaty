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
    public class RolesController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public RolesController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<RolesWriteDto>> GetRoleById(Guid Id)
        {
            var result = await _wrapper.Roles.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var RolesModel = _mapper.Map<RolesWriteDto>(result);
            return Ok(RolesModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<RolesReadDto>> GetAllRoles(int PageNumber, int Count)
        {
            var result = await _wrapper.Roles.FindAll(PageNumber, Count);
            var RolesModel = _mapper.Map<IList<RolesReadDto>>(result);
            return Ok(RolesModel);
        }
        [HttpPost]
        public async Task<ActionResult<RolesReadDto>> AddRole([FromBody] RolesWriteDto RolesWriteDto)
        {
            var RolesModel = _mapper.Map<Roles>(RolesWriteDto);
            await _wrapper.Roles.Create(RolesModel);
            var RolesReadDto = _mapper.Map<RolesReadDto>(RolesModel);
            return Ok(RolesReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid Id, [FromBody] RolesWriteDto RolesWriteDto)
        {
            var RolesModelFromRepo =await _wrapper.Roles.FindById(Id);
            if (RolesModelFromRepo == null)
            {
                return NotFound();
            }
            RolesModelFromRepo.Role = RolesWriteDto.Role;
            _wrapper.Roles.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid Id)
        {
            var Roles =await _wrapper.Roles.Delete(Id);
            if (Roles == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}