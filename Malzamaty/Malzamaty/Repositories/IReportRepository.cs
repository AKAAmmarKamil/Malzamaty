using Malzamaty.Model;
using Malzamaty.Repositories;
namespace Malzamaty.Services
{
    public interface IReportRepository : IBaseRepository<Report>
    {
    }
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly MalzamatyContext _db;
        public ReportRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
    }
}
