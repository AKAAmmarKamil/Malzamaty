using AutoMapper;
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
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly IDistrictService _districtService;
        private readonly IMahallahService _mahallahService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AddressController(IAddressService AddressService, ICountryService countryService,IProvinceService provinceService,IDistrictService districtService,IMahallahService mahallahService,IUserService userService, IMapper mapper)
        {
            _addressService = AddressService;
            _countryService = countryService;
            _provinceService = provinceService;
            _districtService = districtService;
            _mahallahService = mahallahService;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetAddressById")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<AddressWriteDto>> GetAddressById(Guid Id)
        {
            var result = await _addressService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var AddressModel = _mapper.Map<AddressWriteDto>(result);
            return Ok(AddressModel);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<AddressReadDto>> GetAllAddresss()
        {
            var result = await _addressService.GetAll();
            var AddressModel = _mapper.Map<IList<AddressReadDto>>(result);
            return Ok(AddressModel);
        }
        [HttpPost]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> AddAddress([FromBody] AddressWriteDto AddressWriteDto)
        {
            var AddressModel = _mapper.Map<Address>(AddressWriteDto);
            var Result=await _addressService.Create(AddressModel);
            Result.Country =await _countryService.FindById(Result.CountryID);
            Result.Province = await _provinceService.FindById(Result.ProvinceID);
            Result.District = await _districtService.FindById(Result.DistrictID);
            Result.Mahallah = await _mahallahService.FindById(Result.MahallahID);
            var AddressReadDto = _mapper.Map<AddressReadDto>(AddressModel);
            return CreatedAtRoute("GetAddressById", new { Id = AddressReadDto.Id }, AddressReadDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UpdateAddress(Guid Id, [FromBody] AddressWriteDto AddressWriteDto)
        {
            var AddressModelFromRepo = await _addressService.FindById(Id);
            if (AddressModelFromRepo == null)
            {
                return NotFound();
            }
            var AddressModel = _mapper.Map<Address>(AddressWriteDto);
            await _addressService.Modify(Id, AddressModel);
            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> UpdateUserAddress(Guid Id, [FromBody] AddressWriteDto AddressWriteDto)
        {
            var User =await _userService.FindById(Guid.Parse(GetClaim("ID")));
            if (GetClaim("Role") != "Admin" && User.AddressID != Id)
            {
                return BadRequest(new { Error = "لا يمكن تعديل بيانات تخص مستخدم آخر من دون صلاحية المدير" });
            }
            var AddressModelFromRepo = await _addressService.FindById(Id);
            if (AddressModelFromRepo == null)
            {
                return NotFound();
            }
            var AddressModel = _mapper.Map<Address>(AddressWriteDto);
            await _addressService.Modify(Id, AddressModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteAddresss(Guid Id)
        {
            var Address = await _addressService.Delete(Id);
            if (Address == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}