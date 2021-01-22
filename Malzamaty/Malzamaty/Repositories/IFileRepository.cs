﻿using Malzamaty.Dto;
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
        Task<bool> IsExist(string FilePath);
        Task<List<File>> TopRating(Guid Id, bool WithReports);
        Task<List<File>> MostDownloaded(Guid Id, bool WithReports);
        Task<List<File>> NewFiles(Guid Id, bool WithReports);
        Task<List<File>> RelatedFiles(Guid Id);
    }
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        private readonly MalzamatyContext _db;
        public FileRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
        public async Task<File> FindById(Guid id)
        {
            var Result = await _db.File.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).FirstOrDefaultAsync(x => x.ID == id);

            if (Result == null) return null;
            return Result;
        }
        public  async Task< bool> IsExist(string FilePath)
        {
            var File =await _db.File.Where(x => x.FilePath == FilePath).FirstOrDefaultAsync();
            if (File != null)
                 return false;
            return  true;
        }
        public async Task<IEnumerable<File>> FindAll(int PageNumber, int count) => await _db.File.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).Skip((PageNumber - 1) * count).Take(count).ToListAsync();
        public async Task<List<File>> MostDownloaded(Guid Id, bool WithReports)
        {
            var Files = new List<File>();
            if (WithReports == true)
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) && x.User.ID == Id).
                Include(x => x.Report).Include(x => x.User).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                   .OrderByDescending(x => x.DownloadCount).Take(5).ToListAsync();
            else
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) &&
                        !_db.Report.Any(y => y.File.ID == x.ID) && x.User.ID == Id)
                        .Include(x => x.Report).Include(x => x.User).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                        .OrderByDescending(x => x.DownloadCount).Take(5).ToListAsync();
            return Files;
        }
        public async Task<List<File>> NewFiles(Guid Id,bool WithReports)
        {
            var Files = new List<File>();
            if(WithReports==true)
             Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) && x.User.ID == Id).
             Include(x => x.Report).Include(x => x.User).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                .OrderByDescending(x => x.PublishDate).ThenByDescending(x => x.UploadDate).Take(5).ToListAsync();
            else
            Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID)&&
                    !_db.Report.Any(y => y.File.ID == x.ID) && x.User.ID == Id)
                    .Include(x => x.Report).Include(x => x.User).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                    .OrderByDescending(x => x.PublishDate).ThenByDescending(x => x.UploadDate).Take(5).ToListAsync();
            return Files;           
        }
        public async Task<List<File>> TopRating(Guid Id, bool WithReports)
        {
            var Files = new List<File>();
            if (WithReports == true)
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID)) 
                    .Include(h => h.Rating).Include(x => x.Report).Include(x => x.User).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                    .OrderByDescending(x => x.Rating.Average(o => (double?)o.Rate)).Take(5).ToListAsync();
           else
                Files = await _db.File.Where(x => _db.Interests.Any(y => y.ClassID == x.Class.ID && y.SubjectID == x.Subject.ID) &&
                     !_db.Report.Any(y => y.File.ID == x.ID) && x.User.ID == Id)
                     .Include(h => h.Rating).Include(x => x.Report).Include(x => x.User).Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType)
                     .OrderByDescending(x => x.Rating.Average(o => (double?)o.Rate)).Take(5).ToListAsync();
            return Files;
        }
        public async Task<List<File>> RelatedFiles(Guid Id)
        {
            var File =await FindById(Id);
            var Files =await _db.File.Where(x => x.Class.ID == File.Class.ID && x.Subject.ID == File.Subject.ID && x.ID != Id).Include(x => x.Class).Include(x => x.Subject).Include(x=>x.Report).Take(5).ToListAsync();
            return  Files;
        }
    }
}
