using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface IFileRepository : IBaseRepository<File>
    {
    }
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        private readonly MalzamatyContext _db;
        public FileRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }

    }
}
