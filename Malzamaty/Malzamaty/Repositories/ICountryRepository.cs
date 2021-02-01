using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;

namespace Malzamaty.Services
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
    }
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;
        public CountryRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
    }
}
