using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using System;
using System.Threading.Tasks;

namespace Malzamaty.Repository
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        private readonly TheContext _db;
        public FileRepository(TheContext context) : base(context)
        {
            _db = context;
        }

    }
}
