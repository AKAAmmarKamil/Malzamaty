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
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    public class LibraryController : BaseController
    {
        private readonly ILibraryService _LibraryService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        public LibraryController(ILibraryService LibraryService,IAddressService addressService, IMapper mapper)
        {
            _LibraryService = LibraryService;
            _addressService = addressService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetLibraryById")]
        public async Task<ActionResult<LibraryReadDto>> GetLibraryById(Guid Id)
        {
            var result = await _LibraryService.FindById(Id);
            var Address = await _addressService.FindById(result.AddressID);
            if (result == null)
            {
                return NotFound();
            }
            result.Address = Address;
            var LibraryModel = _mapper.Map<LibraryReadDto>(result);
            return Ok(LibraryModel);
        }
        [HttpGet]
        public async Task<ActionResult<LibraryReadDto>> GetAllLibrarys(int PageNumber,int Count)
        {
            var result = _LibraryService.FindAll(PageNumber,Count).Result.ToList();
            var LibraryModel = _mapper.Map<IList<LibraryReadDto>>(result);
            var Address = new Address();
            var AddressModel = new AddressReadDto();
            for (int i = 0; i < result.Count(); i++)
            {
                Address = await _addressService.FindById(result[i].AddressID);
                AddressModel = _mapper.Map<AddressReadDto>(Address);
                LibraryModel[i].Address = AddressModel;
            }
            return Ok(LibraryModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddLibrary([FromBody] LibraryWriteDto LibraryWriteDto)
        {
            var LibraryModel = _mapper.Map<Library>(LibraryWriteDto);
            var Result=await _LibraryService.Create(LibraryModel);
            Result.Address =await _addressService.FindById(Result.AddressID);
            var LibraryReadDto = _mapper.Map<LibraryReadDto>(Result);
            return CreatedAtRoute("GetLibraryById", new { Id = LibraryReadDto.ID }, LibraryReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibrary(Guid Id, [FromBody] LibraryWriteDto LibraryWriteDto)
        {
            var LibraryModelFromRepo = await _LibraryService.FindById(Id);
            if (LibraryModelFromRepo == null)
            {
                return NotFound();
            }
            var LibraryModel = _mapper.Map<Library>(LibraryWriteDto);
            await _LibraryService.Modify(Id, LibraryModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrarys(Guid Id)
        {
            var Library = await _LibraryService.Delete(Id);
            if (Library == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}