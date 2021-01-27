using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IRatingRepository : IBaseRepository<Rating>
    {
        Task<List<Rating>> GetRatingByFile(Guid Id);
    }
    public class RatingRepository : BaseRepository<Rating>, IRatingRepository
    {
        private readonly MalzamatyContext _db;
        public RatingRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }        
        public async Task<List<Rating>> GetRatingByFile(Guid Id)=>
        await _db.Rating.Where(x => x.File.ID == Id).ToListAsync();
    }
}
