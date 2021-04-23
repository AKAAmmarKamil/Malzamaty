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
    public interface IMahallahRepository : IBaseRepository<Mahallah>
    {
        Task<IEnumerable<Mahallah>> GetByDistrict(Guid DistrictId);
    }
    public class MahallahRepository : BaseRepository<Mahallah>, IMahallahRepository
    {
        private readonly MalzamatyContext _db;
        public MahallahRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        public async Task<Mahallah> FindById(Guid Id) => await _db.Mahallah.Include(x => x.District).Where(x => x.Id == Id).FirstOrDefaultAsync();
        public async Task<IEnumerable<Mahallah>> GetByDistrict(Guid DistrictId) => await _db.Mahallah.Include(x=>x.District).Where(x=>x.DistrictID== DistrictId).ToListAsync();
    }
}
