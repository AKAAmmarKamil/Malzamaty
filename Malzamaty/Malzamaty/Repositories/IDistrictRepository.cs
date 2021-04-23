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
    public interface IDistrictRepository : IBaseRepository<District>
    {
        Task<IEnumerable<District>> GetByProvince(Guid ProvinceId);
    }
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        private readonly MalzamatyContext _db;
        public DistrictRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        public async Task<District> FindById(Guid Id) => await _db.District.Include(x => x.Province).Where(x => x.Id == Id).FirstOrDefaultAsync();
        public async Task<IEnumerable<District>> GetByProvince(Guid ProvinceId) => await _db.District.Include(x=>x.Province).Where(x=>x.ProvinceID==ProvinceId).ToListAsync();
    }
}
