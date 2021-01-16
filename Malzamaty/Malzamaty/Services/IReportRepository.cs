using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        Task<Report> GetById(Guid id);
        Task<List<Report>> GetAll(int PageNumber, int count);
    }
}
