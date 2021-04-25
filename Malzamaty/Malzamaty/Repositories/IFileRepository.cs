﻿using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IFileRepository : IBaseRepository<File>
    {
        Task<List<File>> TopRating(Guid Id, bool WithReports);
        Task<List<File>> GetByName(string FileName);
        Task<List<File>> MostOrdered(Guid Id, bool WithReports);
        Task<List<File>> NewFiles(Guid Id, bool WithReports);
        Task<List<File>> RelatedFiles(Guid Id);
        Task<File> GetAppropriateFile(Guid Id);
    }
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        private readonly MalzamatyContext _db;
        public FileRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        public async Task<File> FindById(Guid id)
        {
            var Result = await _db.File.Include(x => x.Subject).Include(x => x.Author).Include(x => x.Class)
            .ThenInclude(x => x.ClassType).Include(x => x.Class).ThenInclude(x => x.Stage).FirstOrDefaultAsync(x => x.ID == id);

            if (Result == null) return null;
            return Result;
        }
        public async Task<IEnumerable<File>> FindAll(int PageNumber, int count) => await _db.File.Include(x => x.Subject).Include(x => x.Author).Include(x => x.Class)
            .ThenInclude(x => x.ClassType).Include(x => x.Class).ThenInclude(x => x.Stage).Skip((PageNumber - 1) * count).Take(count).ToListAsync();
        public async Task<List<File>> MostOrdered(Guid Id, bool WithReports)
        {
            var Files = new List<File>();
            if (WithReports == true)
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) && x.Author.ID == Id).
                Include(x => x.Report).Include(x => x.Author).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                   .OrderByDescending(x => x.OrderCount).Take(5).ToListAsync();
            else
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) &&
                        !_db.Report.Any(y => y.File.ID == x.ID) && x.Author.ID == Id)
                        .Include(x => x.Report).Include(x => x.Author).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                        .OrderByDescending(x => x.OrderCount).Take(5).ToListAsync();
            return Files;
        }
        public async Task<List<File>> NewFiles(Guid Id, bool WithReports)
        {
            var Files = new List<File>();
            if (WithReports == true)
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) && x.Author.ID == Id).
                Include(x => x.Report).Include(x => x.Author).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                   .OrderByDescending(x => x.PublishDate).ThenByDescending(x => x.UploadDate).Take(5).ToListAsync();
            else
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) &&
                        !_db.Report.Any(y => y.File.ID == x.ID) && x.Author.ID == Id)
                        .Include(x => x.Report).Include(x => x.Author).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                        .OrderByDescending(x => x.PublishDate).ThenByDescending(x => x.UploadDate).Take(5).ToListAsync();
            return Files;
        }
        public async Task<List<File>> TopRating(Guid Id, bool WithReports)
        {
            var Files = new List<File>();
            if (WithReports == true)
                Files = await _db.File.Where(x => x.Rating.Any() && _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID))
                    .Include(h => h.Rating).Include(x => x.Report).Include(x => x.Author).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                    .OrderByDescending(x => x.Rating.Average(o => (double?)o.Rate)).Take(5).ToListAsync();
            else
                Files = await _db.File.Where(x => x.Rating.Any() && _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) &&
                     !_db.Report.Any(y => y.File.ID == x.ID) && x.Author.ID == Id)
                     .Include(h => h.Rating).Include(x => x.Report).Include(x => x.Author).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                     .OrderByDescending(x => x.Rating.Average(o => (double?)o.Rate)).Take(5).ToListAsync();
            return Files;
        }
        public async Task<File> GetAppropriateFile(Guid Id)
        {
            var Files = await _db.File.Where(x => x.Rating.Any() && _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) &&
                     !_db.Report.Any(y => y.File.ID == x.ID) && x.Author.ID == Id)
                     .Include(h => h.Rating).Include(x => x.Report).Include(x => x.Author).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                     .OrderByDescending(x => x.Rating.Average(o => (double?)o.Rate)).ThenByDescending(x=>x.OrderCount).ThenByDescending(x=>x.PublishDate).ThenByDescending(x=>x.UploadDate).FirstOrDefaultAsync();
            return Files;
        }
        public async Task<List<File>> RelatedFiles(Guid Id)
        {
            var File = await FindById(Id);
            var Files = await _db.File.Where(x => x.Class.ID == File.Class.ID && x.Subject.ID == File.Subject.ID && x.ID != Id).Include(x => x.Class).Include(x => x.Subject).Include(x => x.Report).Take(5).ToListAsync();
            return Files;
        }

        public async Task<List<File>> GetByName(string FileName)=> await _db.File.Include(x => x.Subject).Include(x => x.Author).Include(x => x.Class)
            .ThenInclude(x => x.ClassType).Include(x => x.Class).ThenInclude(x => x.Stage).Where(x=>x.Description.Contains(FileName)).ToListAsync();
    }
}
