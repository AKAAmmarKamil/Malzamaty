using Malzamaty.Model;
using Malzamaty.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Repository
{
    public class StageRepository : BaseRepository<Stage>, IStageRepository
    {
        private readonly TheContext _db;
        public StageRepository(TheContext context) : base(context)
        {
            _db = context;
        }
    }
}
