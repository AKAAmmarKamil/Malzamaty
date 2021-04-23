using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<IEnumerable<Address>> GetAll();
    }
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly MalzamatyContext _db;
        public AddressRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        public async Task<Address> FindById(Guid Id) => await _db.Address.Include(x => x.Country).Include(x => x.Province).Include(x => x.District).Include(x => x.Mahallah).FirstOrDefaultAsync(x=>x.Id==Id);
        public async Task<IEnumerable<Address>> GetAll() => await _db.Address.Include(x => x.Country).Include(x=>x.Province).Include(x=>x.District).Include(x=>x.Mahallah).ToListAsync();

    }
}
