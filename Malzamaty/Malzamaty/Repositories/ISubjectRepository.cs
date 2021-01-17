using Malzamaty.Model;
using Malzamaty.Repositories;
namespace Malzamaty
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
    }
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly MalzamatyContext _db;
        public SubjectRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }

    }
}
