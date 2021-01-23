using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ReportController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public ReportController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}",Name = "GetReportById")]
        public async Task<ActionResult<ReportReadDto>> GetReportById(Guid Id)
        {
            var result = await _wrapper.Report.FindById(Id);
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
            var result =_wrapper.Report.FindAll(PageNumber,Count).Result.ToList();
            var ReportModel = _mapper.Map<List<ReportReadDto>>(result);
            return Ok(ReportModel);
        }
        [HttpPost]
        public async Task<ActionResult<ReportReadDto>> AddReport([FromBody] ReportWriteDto ReportWriteDto)
        {
            var ReportModel = _mapper.Map<Report>(ReportWriteDto);
            ReportModel.File =await _wrapper.File.FindById(ReportWriteDto.FileID);
            await _wrapper.Report.Create(ReportModel);
            var Result = _wrapper.Report.FindById(ReportModel.ID);
            var ReportReadDto = _mapper.Map<ReportReadDto>(Result.Result);
            return CreatedAtRoute("GetReportById", new { Id = ReportReadDto.Id }, ReportReadDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(Guid Id, [FromBody] ReportWriteDto ReportWriteDto)
        {
            var ReportModelFromRepo = await _wrapper.Report.FindById(Id);
            if (ReportModelFromRepo == null)
            {
                return NotFound();
            }
            ReportModelFromRepo.Description = ReportWriteDto.Description;
            _wrapper.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportes(Guid Id)
        {
            var Report = await _wrapper.Report.Delete(Id);
            if (Report == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}