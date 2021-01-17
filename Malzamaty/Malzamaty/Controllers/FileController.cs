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
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        private readonly UploadFile _uploadFile;
        public FileController(IRepositoryWrapper wrapper, IMapper mapper,UploadFile uploadFile)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _uploadFile =uploadFile;
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
            var result = _wrapper.File.FindAll(PageNumber, Count).Result.ToList();
            var FileModel = _mapper.Map<IList<FileReadDto>>(result);
            return Ok(FileModel);
        }
        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(string @Path)
        {
            var Result=await _uploadFile.Upload(@Path);
            return Result;
        }
        [HttpPost]
        public async Task<ActionResult<FileReadDto>> AddFile([FromBody] FileWriteDto FileWriteDto)
        {
            var FileModel = _mapper.Map<File>(FileWriteDto);
            await _wrapper.File.Create(FileModel);
            var Result = _wrapper.File.FindById(FileModel.ID);
            var FileReadDto = _mapper.Map<FileReadDto>(Result.Result);
            return Ok(FileReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);
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