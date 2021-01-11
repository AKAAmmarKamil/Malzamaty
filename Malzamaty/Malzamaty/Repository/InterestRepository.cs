﻿using Malzamaty.Model;
using Malzamaty.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Malzamaty.Repository
{
    public class InterestsRepository : BaseRepository<Interests>, IInterestRepository
    {
        private readonly TheContext _db;

        public InterestsRepository(TheContext context) : base(context)
        {
            _db = context;
        }
        public async Task<List<Interests>> GetAll(int PageNumber, int count)
        {
            var Interests = await _db.Interests.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).Skip((PageNumber - 1) * count).Take(count).ToListAsync();
            return Interests;
        }
       
        public async Task<List<Interests>> GetInterests(Guid Id)
        {
            var Result=await  _db.Interests.Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType).Where(x => x.U_ID == Id).ToListAsync();
            return Result;
        }
        public async Task<Interests> Create(Interests t)
        {
            await _db.Interests.AddAsync(t);
            await _db.SaveChangesAsync();
            return t;
        }
        public async Task<Interests> Delete(Guid id)
        {
            var result = await FindById(id);
            if (result == null) return null;
            _db.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        
        public async Task<Interests> FindById(Guid id)
        {
            var Result = _db.Interests.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).FirstOrDefaultAsync(x => x.ID == id);

            if (Result == null) return null;
            return await Result;
        }
    }
}