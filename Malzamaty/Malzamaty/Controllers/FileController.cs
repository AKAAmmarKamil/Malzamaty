using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Microsoft.AspNetCore.Mvc;
using File = Malzamaty.Model.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Malzamaty.Form;
using System.Linq;
using Malzamaty.Model;

namespace Malzamaty.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        private IHostingEnvironment _environment;

        public FileController(IHostingEnvironment environment, IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _environment = environment;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<FileReadDto>> GetFileById(Guid Id)
        {
            var result = await _wrapper.File.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var FileModel = _mapper.Map<FileReadDto>(result);
            return Ok(FileModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<FileReadDto>> GetAllFiles(int PageNumber, int Count)
        {
            var result =await _wrapper.File.FindAll(PageNumber, Count);
            var FileModel = _mapper.Map<IList<FileReadDto>>(result);
            return Ok(FileModel);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<FileReadDto>> MostDownloadedFiles()
        {
            var User = GetClaim("ID");           
            var result = await _wrapper.File.MostDownloaded(Guid.Parse(User));
            var FileModel = _mapper.Map<IList<FileReadDto>>(result);
            return Ok(FileModel);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<FileReadDto>> RelatedFiles(Guid Id)
        {
            var result = await _wrapper.File.RelatedFiles(Id);
            var FileModel = _mapper.Map<IList<FileReadDto>>(result);
            return Ok(FileModel);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<FileReadDto>> NewFiles(bool WithReports)
        {
            var User = GetClaim("ID");
            var result = await _wrapper.File.NewFiles(Guid.Parse(User),WithReports);
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
        public async Task<ActionResult<AttachmentString>> AddAttachment(string Path)
        {
            _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Path = Path.Replace("\\", @"\");
            var bytes = await Attachment.Attachment.ConvertToBytes(Path);
            var Type = Path.Split(".")[1];
            var FullPath = _environment.WebRootPath;
            var Upload = await Attachment.Attachment.Upload(bytes, FullPath,Type);
            return Ok(Upload);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<FileReadDto>> AddFile([FromBody] FileWriteDto FileWriteDto)
        {
            var FileModel = _mapper.Map<File>(FileWriteDto);
            FileModel.User =await _wrapper.User.FindById(Guid.Parse(GetClaim("ID")));
            FileModel.Class = await _wrapper.Class.FindById(FileWriteDto.Class);
            FileModel.Subject = await _wrapper.Subject.FindById(FileWriteDto.Subject);
            _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Console.WriteLine(_environment.WebRootPath);
            var FullPath = _environment.WebRootPath + FileWriteDto.FilePath;
            var File =await _wrapper.File.IsExist(FileWriteDto.FilePath);
            if (File == false)
            {
                return BadRequest(new { Error = "لا يمكن إضافة ملف موجود مسبقاً" });
            }
            if (System.IO.File.Exists(FullPath))
            {
                await _wrapper.File.Create(FileModel);
                var Result = await _wrapper.File.FindById(FileModel.ID);
                var FileReadDto = _mapper.Map<FileReadDto>(Result);
                return Ok(FileReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
            }    
            return BadRequest(new { Error="الملف لم يتم تحميله" });
        }
        [HttpGet]
        public async Task<FileStreamResult> DownloadFile(Guid Id)
        {
            var File = await _wrapper.File.FindById(Id);
            var Path = File.FilePath;
            _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Console.WriteLine(_environment.WebRootPath);
            var FullPath = _environment.WebRootPath +Path;
            var Type = Path.Split(".")[1];
            if (File != null&& System.IO.File.Exists(FullPath))
            {
                File.DownloadCount = Convert.ToInt32(File.DownloadCount) + 1;
                _wrapper.Save();
                var result = await Attachment.Attachment.ConvertToBytes(FullPath);
                return new FileStreamResult(new MemoryStream(result), "application/" + Type.ToString());
            }
            return null;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFile(Guid Id, [FromBody] FileUpdateDto FileUpdateDto)
        {
            var FileModelFromRepo = await _wrapper.File.FindById(Id);
            if (FileModelFromRepo == null)
            {
                return NotFound();
            }
            FileModelFromRepo.Description = FileUpdateDto.Description;
            FileModelFromRepo.Author = FileUpdateDto.Author;
            FileModelFromRepo.Type = FileUpdateDto.Type;
            _wrapper.User.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiles(Guid Id)
        {
            var File = await _wrapper.File.FindById(Id);
            var Path = File.FilePath;
            _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Console.WriteLine(_environment.WebRootPath);
            var FullPath = _environment.WebRootPath + Path;
            var Result = await _wrapper.File.Delete(Id);
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