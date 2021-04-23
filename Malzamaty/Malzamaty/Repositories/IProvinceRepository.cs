using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface IProvinceRepository : IBaseRepository<Province>
    {
        Task<IEnumerable<Province>> GetByCountry(Guid CountryId);
    }
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        private readonly MalzamatyContext _db;
        public ProvinceRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

        public async Task<IEnumerable<Province>> GetByCountry(Guid CountryId) => await _db.Province.Include(x=>x.Country).Where(x=>x.CountryID==CountryId).ToListAsync();

    }
}
