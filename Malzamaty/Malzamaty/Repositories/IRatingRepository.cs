using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using System;

namespace Malzamaty.Services
{
    public interface IRatingRepository : IBaseRepository<Rating>
    {
    }
    public class RatingRepository : BaseRepository<Rating>, IRatingRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly Mapper _mapper;
        public RatingRepository(MalzamatyContext context, Mapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        
    }
}
