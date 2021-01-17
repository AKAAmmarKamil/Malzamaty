using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface IClassTypeRepository : IBaseRepository<ClassType>
    {
    }
    public class ClassTypeRepository : BaseRepository<ClassType>, IClassTypeRepository
    {
        private readonly MalzamatyContext _db;
        public ClassTypeRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
    }
}
