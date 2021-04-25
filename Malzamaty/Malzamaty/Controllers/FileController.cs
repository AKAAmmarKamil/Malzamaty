using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Form;
using Malzamaty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = Malzamaty.Model.File;

namespace Malzamaty.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly ISubjectService _subjectService;
        private readonly IClassService _classService;
        private readonly ILibraryService _libraryService;
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly ITaxesService _taxesService;
        private readonly IMapper _mapper;
        private IHostingEnvironment _environment;
        [Obsolete]
        public FileController(IHostingEnvironment environment, IFileService fileService, IUserService userService,
            ISubjectService subjectService, IClassService classService,ILibraryService libraryService,IOrderService orderService,
            IAddressService addressService,ITaxesService taxesService, IMapper mapper)
        {
            _fileService = fileService;
            _userService = userService;
            _subjectService = subjectService;
            _classService = classService;
            _libraryService = libraryService;
            _orderService = orderService;
            _addressService = addressService;
            _taxesService = taxesService;
            _mapper = mapper;
            _environment = environment;
        }
        [HttpGet("{Id}", Name = "GetFileById")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> GetFileById(Guid Id)
        {
            var result = await _fileService.FindById(Id);
            result.Price = await _taxesService.GetFinalPrice(result.ID);
            if (result == null)
            {
                return NotFound();
            }
            var FileModel = _mapper.Map<FileReadDto>(result);
            return Ok(FileModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<FileReadDto>> GetAllFiles(int PageNumber, int Count)
        {
            var result = _fileService.All(PageNumber, Count).Result.ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Price = await _taxesService.GetFinalPrice(result[i].ID);
            }
            var FileModel = _mapper.Map<IList<FileReadDto>>(result);
            return Ok(FileModel);
        }
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetYears()
        {
            var result = await _fileService.GetYears();
            return Ok(result);
        }
        [HttpGet("{FileName}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<FileReadDto>> GetByName(string FileName)
        {
            var result = await _fileService.GetByName(FileName);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Price = await _taxesService.GetFinalPrice(result[i].ID);
            }
            var FileModel = _mapper.Map<IList<FileReadDto>>(result);
            return Ok(FileModel);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> TopRatingFiles(bool WithReports)
        {
            var User = GetClaim("ID");
            var result = await _fileService.TopRating(Guid.Parse(User), WithReports);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Price = await _taxesService.GetFinalPrice(result[i].ID);
            }
            if (WithReports == true)
            {
                var FileReadDto = _mapper.Map<List<FileWithReportsAndRatingReadDto>>(result);
                return Ok(FileReadDto);
            }
            else
            {
                var FileReadDto = _mapper.Map<List<FileWithRatingReadDto>>(result);
                return Ok(FileReadDto);
            }
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> GetAppropriateFile()
        {
            var User = GetClaim("ID");
            var result = await _fileService.GetAppropriateFile(Guid.Parse(User));
            result.Price = await _taxesService.GetFinalPrice(result.ID);
            var FileReadDto = _mapper.Map<FileWithRatingReadDto>(result);
            return Ok(FileReadDto);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> MostOrderedFiles(bool WithReports)
        {
            var User = GetClaim("ID");
            var result = await _fileService.MostOrdered(Guid.Parse(User), WithReports);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Price = await _taxesService.GetFinalPrice(result[i].ID);
            }
            if (WithReports == true)
            {
                var FileWithReportsReadDto = _mapper.Map<List<FileWithReportsReadDto>>(result);
                return Ok(FileWithReportsReadDto);
            }
            else
            {
                var FileReadDto = _mapper.Map<List<FileReadDto>>(result);
                return Ok(FileReadDto);
            }
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> RelatedFiles(Guid Id)
        {
            var result = await _fileService.RelatedFiles(Id);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Price = await _taxesService.GetFinalPrice(result[i].ID);
            }
            var FileModel = _mapper.Map<List<FileWithReportsReadDto>>(result);
            return Ok(FileModel);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> NewFiles(bool WithReports)
        {
            var User = GetClaim("ID");
            var result = await _fileService.NewFiles(Guid.Parse(User), WithReports);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Price = await _taxesService.GetFinalPrice(result[i].ID);
            }
            if (WithReports == true)
            {
                var FileWithReportsReadDto = _mapper.Map<List<FileWithReportsReadDto>>(result);
                return Ok(FileWithReportsReadDto);
            }
            else
            {
                var FileReadDto = _mapper.Map<List<FileReadDto>>(result);
                return Ok(FileReadDto);
            }
        }
        [HttpPost]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> AddFile([FromBody] FileWriteDto FileWriteDto)
        {
            var FileModel = _mapper.Map<File>(FileWriteDto);
            FileModel.Author = await _userService.FindById(Guid.Parse(GetClaim("ID")));
            FileModel.Class = await _classService.FindById(FileWriteDto.Class);
            FileModel.Subject = await _subjectService.FindById(FileWriteDto.Subject);
            var Result = await _fileService.Create(FileModel);
            var FileReadDto = _mapper.Map<FileReadDto>(Result);
            return CreatedAtRoute("GetFileById", new { Id = FileReadDto.Id }, FileReadDto);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> OrderFile(OrderFile OrderFile)
        {
            var File = await _fileService.FindById(OrderFile.FileId);
            var User = await _userService.FindById(Guid.Parse(GetClaim("ID")));
            var Library = await _libraryService.FindById(File.LibraryID.GetValueOrDefault());
            var LibraryAddress = await _addressService.FindById(Library.AddressID);
            var UserAddress = await _addressService.FindById(User.AddressID.GetValueOrDefault());
            var Order = new Model.Order();
            await _fileService.ModifyOrderCount(OrderFile.FileId);
            Order.LibraryAddress = LibraryAddress;
            Order.UserAddress = UserAddress;
            File.Price =await _taxesService.GetFinalPrice(File.ID);
            Order.File = File;
            Order.OrderStatus = 0;
            Order.OrderedDate = DateTime.Now;
            var Result=await _orderService.Create(Order);
            var OrderFileReadDto = _mapper.Map<OrderFileReadDto>(Result);
            return Ok(OrderFileReadDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UpdateFile(Guid Id, [FromBody] FileUpdateDto FileUpdateDto)
        {
            var FileModelFromRepo = await _fileService.FindById(Id);
            if (FileModelFromRepo == null)
            {
                return NotFound();
            }
            var FileModel = _mapper.Map<File>(FileUpdateDto);
            await _fileService.Modify(Id, FileModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteFiles(Guid Id)
        {
            var Result = await _fileService.Delete(Id);
            if (Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}