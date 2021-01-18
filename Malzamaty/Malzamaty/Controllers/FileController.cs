using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using File = Malzamaty.Model.File;
using Malzamaty.Attachment;
using Malzamaty.Form;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        protected string GetClaim(string claimName)
        {
            return (User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(c =>
                string.Equals(c.Type, claimName, StringComparison.CurrentCultureIgnoreCase))?.Value;
        }
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
        [HttpPost("AddAttachment")]
        public async Task<IActionResult> AddAttachment([FromBody] AttachmentString attachment)
        {
            //if (attachment == null || attachment.Body == null || !UploadFile.IsBase64(attachment.Body))
            //{
            //    return StatusCode(400, "attachment is invalid");
            //}
            //var attachmentId =await _uploadFile.Upload(attachment.Body);
            return Ok(/*attachmentId*/);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<FileReadDto>> AddFile([FromBody] FileWriteDto FileWriteDto)
        {
            var UserId =GetClaim("ID");
            var FileModel = _mapper.Map<File>(FileWriteDto);
            FileModel.User =await _wrapper.User.FindById(Guid.Parse(UserId));
            FileModel.Subject = await _wrapper.Subject.FindById(FileWriteDto.Subject);
            FileModel.Class = await _wrapper.Class.FindById(FileWriteDto.Class);
            await _wrapper.File.Create(FileModel);
            var Result =await _wrapper.File.FindById(FileModel.ID);
            var FileReadDto = _mapper.Map<FileReadDto>(Result);
            return Ok(FileReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
        }
        [HttpGet]
        public async Task<FileStreamResult> DownloadFile(string Path)
        {
            _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\").Replace("\\", @"\");
            Console.WriteLine(_environment.WebRootPath);
            var FullPath = _environment.WebRootPath +Path;
            var Type = Path.Split(".")[1];
            var File =await _wrapper.File.FindByPath(FullPath);
            if (File != null)
            {
                File.DownloadCount = Convert.ToInt32(File.DownloadCount) + 1;
                _wrapper.Save();
                byte[] result = System.IO.File.ReadAllBytes(FullPath);
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
        public async Task<IActionResult> DeleteFilees(Guid Id)
        {
            var File = await _wrapper.File.Delete(Id);
            if (File == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}