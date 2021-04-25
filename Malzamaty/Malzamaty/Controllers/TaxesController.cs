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
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    public class TaxesController : BaseController
    {
        private readonly ITaxesService _TaxesService;
        private readonly IMapper _mapper;
        public TaxesController(ITaxesService TaxesService, IMapper mapper)
        {
            _TaxesService = TaxesService;
            _mapper = mapper;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaxes(Guid Id, [FromBody] TaxesWriteDto TaxesWriteDto)
        {
            var TaxesModelFromRepo = await _TaxesService.FindById(Id);
            if (TaxesModelFromRepo == null)
            {
                return NotFound();
            }
            var TaxesModel = _mapper.Map<Taxes>(TaxesWriteDto);
            await _TaxesService.Modify(Id, TaxesModel);
            return NoContent();
        }
    }
}