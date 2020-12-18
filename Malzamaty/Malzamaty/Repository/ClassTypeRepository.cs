using Malzamaty.Model;
using Malzamaty.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Repository
{
    public class ClassTypeRepository : BaseRepository<ClassType>, IClassTypeRepository
    {
        private readonly TheContext _db;
        public ClassTypeRepository(TheContext context) : base(context)
        {
            _db = context;
        }
    }
}
