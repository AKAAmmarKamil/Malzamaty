using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Malzamaty.Model;
using Malzamaty.Dto;
using Malzamaty.Services;
using System.Collections.Generic;
using System.Linq;

namespace Malzamaty.Repository
{
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        private readonly TheContext _db;
        public RolesRepository(TheContext context) : base(context)
        {
            _db = context;
        }
       
    }
}
