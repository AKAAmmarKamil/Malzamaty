using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
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
        public async Task<ActionResult<ReportReadDto>> GetAllReports(int PageNumber,int Count)
        {
            var result =await _reportService.All(PageNumber,Count);
            var ReportModel = _mapper.Map<IList<ReportReadDto>>(result);
            return Ok(ReportModel);
        }
        [HttpPost]
        public async Task<ActionResult<ReportReadDto>> AddReport([FromBody] ReportWriteDto ReportWriteDto)
        {
            var ReportModel = _mapper.Map<Report>(ReportWriteDto);
            ReportModel.File =await _fileService.FindById(ReportWriteDto.FileID);
            var Result = await _reportService.Create(ReportModel);
            var ReportReadDto = _mapper.Map<ReportReadDto>(Result);
            return CreatedAtRoute("GetReportById", new { Id = ReportReadDto.Id }, ReportReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(Guid Id, [FromBody] ReportWriteDto ReportWriteDto)
        {
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