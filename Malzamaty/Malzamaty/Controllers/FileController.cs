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
        private readonly IRatingService _ratingService;
        private readonly IClassService _classService;
        private readonly IMapper _mapper;
        private IHostingEnvironment _environment;

        [Obsolete]
        public FileController(IHostingEnvironment environment, IFileService fileService, IUserService userService,
            ISubjectService subjectService, IClassService classService, IRatingService ratingService, IMapper mapper)
        {
            _fileService = fileService;
            _userService = userService;
            _subjectService = subjectService;
            _classService = classService;
            _mapper = mapper;
            _environment = environment;
            _ratingService = ratingService;
        }
        [HttpGet("{Id}", Name = "GetFileById")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> GetFileById(Guid Id)
        {
            var result = await _fileService.FindById(Id);
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
            var result = await _fileService.All(PageNumber, Count);
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
            var FileModel = _mapper.Map<IList<FileReadDto>>(result);
            return Ok(FileModel);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> TopRatingFiles(bool WithReports)
        {
            var User = GetClaim("ID");
            var result = await _fileService.TopRating(Guid.Parse(User), WithReports);
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
            var FileReadDto = _mapper.Map<FileWithRatingReadDto>(result);
            return Ok(FileReadDto);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> MostDownloadedFiles(bool WithReports)
        {
            var User = GetClaim("ID");
            var result = await _fileService.MostDownloaded(Guid.Parse(User), WithReports);
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
            var FileModel = _mapper.Map<List<FileWithReportsReadDto>>(result);
            return Ok(FileModel);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<FileReadDto>> NewFiles(bool WithReports)
        {
            var User = GetClaim("ID");
            var result = await _fileService.NewFiles(Guid.Parse(User), WithReports);
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
        public async Task<IActionResult> AddAttachment(string Path)
        {
            _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Path = Path.Replace("\\", @"\");
            var bytes = await Attachment.Attachment.ConvertToBytes(Path);
            var Type = Path.Split(".")[1];
            var FullPath = _environment.WebRootPath;
            var Upload = await Attachment.Attachment.Upload(bytes, FullPath, Type);
            return Ok(Upload);
        }
        [HttpPost]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> AddFile([FromBody] FileWriteDto FileWriteDto)
        {
            var FileModel = _mapper.Map<File>(FileWriteDto);
            FileModel.User = await _userService.FindById(Guid.Parse(GetClaim("ID")));
            FileModel.Class = await _classService.FindById(FileWriteDto.Class);
            FileModel.Subject = await _subjectService.FindById(FileWriteDto.Subject);
            _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Console.WriteLine(_environment.WebRootPath);
            var FullPath = _environment.WebRootPath + FileWriteDto.FilePath;
            var File = await _fileService.IsExist(FileWriteDto.FilePath);
            if (File == false)
            return BadRequest(new { Error = "لا يمكن إضافة ملف موجود مسبقاً"});
            if (System.IO.File.Exists(FullPath))
            {
                var Result = await _fileService.Create(FileModel);
                var FileReadDto = _mapper.Map<FileReadDto>(Result);
                return CreatedAtRoute("GetFileById", new { Id = FileReadDto.Id }, FileReadDto);
            }
            return BadRequest(new { Error = "الملف لم يتم تحميله" });
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<FileStreamResult> DownloadFile(Guid Id)
        {
            var File = await _fileService.FindById(Id);
            var Path = File.FilePath;
            _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Console.WriteLine(_environment.WebRootPath);
            var FullPath = _environment.WebRootPath + Path;
            var Type = Path.Split(".")[1];
            if (File != null && System.IO.File.Exists(FullPath))
            {
                await _fileService.ModifyDownloadCount(Id);
                var result = await Attachment.Attachment.ConvertToBytes(FullPath);
                return new FileStreamResult(new MemoryStream(result), "application/" + Type.ToString());
            }
            return null;
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
            var File = await _fileService.FindById(Id);
            var Path = File.FilePath;
            _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Console.WriteLine(_environment.WebRootPath);
            var FullPath = _environment.WebRootPath + Path;
            var Result = await _fileService.Delete(Id);
            if (Result == null)
            {
                return NotFound();
            }
            if (System.IO.File.Exists(FullPath))
            {
                System.IO.File.Delete(FullPath);
            }
            return NoContent();
        }
    }
}