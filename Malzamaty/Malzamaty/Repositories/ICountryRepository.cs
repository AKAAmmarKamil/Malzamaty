using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
        Task<IEnumerable<Country>> GetAll();
    }
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;
        public CountryRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

        public async Task<IEnumerable<Country>> GetAll()=> await _db.Country.ToListAsync();
    }
}
