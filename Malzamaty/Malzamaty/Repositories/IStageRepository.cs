using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using System;

namespace Malzamaty.Services
{
    public interface IStageRepository : IBaseRepository<Stage>
    {
    }
    public class StageRepository : BaseRepository<Stage>, IStageRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;
        public StageRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
    }
}
