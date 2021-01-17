using Malzamaty.Model;
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
        public CountryRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
    }
}
