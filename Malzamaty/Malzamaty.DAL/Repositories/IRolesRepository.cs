using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface IRolesRepository : IBaseRepository<Roles>
    {
    }
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        private readonly MalzamatyContext _db;
        public RolesRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }

    }
}
