using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Malzamaty.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace Malzamaty
{
    public interface ITaxesRepository : IBaseRepository<Taxes>
    {
        Task<IEnumerable<Taxes>> GetAll();
        Task<double> GetFinalPrice(Guid FileId);
    }
    public class TaxesRepository : BaseRepository<Taxes>, ITaxesRepository
    {
        private readonly MalzamatyContext _db;
        private readonly IFileRepository _fileRepository;

        public TaxesRepository(MalzamatyContext context,IFileRepository fileRepository, IMapper mapper) : base(context, mapper)
        {
            _db = context;
            _fileRepository = fileRepository;
        }

        public async Task<IEnumerable<Taxes>> GetAll() => await _db.Taxes.ToListAsync();

        public async Task<double> GetFinalPrice(Guid FileId)
        {
            var File =await _fileRepository.FindById(FileId);
            var Taxes = GetAll().Result.ToList();
            var result = File.Price + (Taxes[0].DeliveryTaxes - (Taxes[0].DeliveryTaxes * Taxes[0].DeliveryDiscount / 100)) + (Taxes[0].MalzamatyTaxes - (Taxes[0].MalzamatyTaxes * Taxes[0].MalzamatyDiscount / 100));
            return result;
        }
    }
}
