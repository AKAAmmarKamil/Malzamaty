using Malzamaty.Model;
using Malzamaty.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Repository
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private readonly TheContext _db;
        public CountryRepository(TheContext context) : base(context)
        {
            _db = context;
        }
    }
}
