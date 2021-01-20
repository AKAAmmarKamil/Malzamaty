﻿using Malzamaty.Model;
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
        Task<IQueryable<File>> MostDownloaded(Guid Id);
        Task<IQueryable<File>> NewFiles(Guid Id);
        Task<IQueryable<File>> RelatedFiles(Guid Id);
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
        public async Task<IQueryable<File>> MostDownloaded(Guid Id)
        {
            var Files= from f in _db.File
                   where _db.Interests.Any(gi => gi.C_ID == f.Class.ID &&gi.Su_ID==f.Subject.ID)&&
                   f.User.ID== Id
                   select f;
            return  Files.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).OrderByDescending(x=>x.DownloadCount).Take(5);
        }
        public async Task<IQueryable<File>> NewFiles(Guid Id)
        {
            var Files = from f in _db.File
                        where _db.Interests.Any(gi => gi.C_ID == f.Class.ID && gi.Su_ID == f.Subject.ID) &&
                        f.User.ID == Id
                        select f;
            return Files.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).OrderByDescending(x => x.PublishDate).ThenByDescending(x=>x.UploadDate).Take(5);
        }
        public async Task<IQueryable<File>> RelatedFiles(Guid Id)
        {
            var File =await FindById(Id);
            var Files = from f in _db.File
                        where f.Class.ID==File.Class.ID && f.Subject.ID==File.Subject.ID
                        && f.ID!=Id
                        select f;
            return Files.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).Take(5);
        }
    }
}
