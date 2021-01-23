using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
    }
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;
        public CountryRepository(MalzamatyContext context,IMapper mapper) : base(context,mapper)
        {
            _db = context;
        }
    }
}
