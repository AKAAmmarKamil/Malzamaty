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
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public ReportController(IReportService reportService,IFileService fileService, IMapper mapper)
        {
            _reportService = reportService;
            _fileService = fileService;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetReportById")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<ReportReadDto>> GetReportById(Guid Id)
        {
            var result = await _reportService.FindById(Id);
            if (result == null)
            {
                return NotFound();
            }
            var ReportModel = _mapper.Map<ReportReadDto>(result);
            return Ok(ReportModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<ReportReadDto>> GetAllReports(int PageNumber,int Count)
        {
            var result =await _reportService.All(PageNumber,Count);
            var ReportModel = _mapper.Map<IList<ReportReadDto>>(result);
            return Ok(ReportModel);
        }
        [HttpPost]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<ReportReadDto>> AddReport([FromBody] ReportWriteDto ReportWriteDto)
        {
            var ReportModel = _mapper.Map<Report>(ReportWriteDto);
            ReportModel.File =await _fileService.FindById(ReportWriteDto.FileID);
            var Result = await _reportService.Create(ReportModel);
            var ReportReadDto = _mapper.Map<ReportReadDto>(Result);
            return CreatedAtRoute("GetReportById", new { Id = ReportReadDto.Id }, ReportReadDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> UpdateReport(Guid Id, [FromBody] ReportWriteDto ReportWriteDto)
        {
            if (GetClaim("Role") != "Admin" && GetClaim("ID") != Id.ToString())
            {
                return BadRequest(new { Error = "لا يمكن تعديل بيانات تخص مستخدم آخر من دون صلاحية المدير" });
            }
            var ReportModelFromRepo = await _reportService.FindById(Id);
            if (ReportModelFromRepo == null)
            {
                return NotFound();
            }
            var ReportModel = _mapper.Map<Report>(ReportWriteDto);
            await _reportService.Modify(Id,ReportModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> DeleteReportes(Guid Id)
        {
            var Report = await _reportService.Delete(Id);
            if (Report == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}