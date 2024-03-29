using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IReportService : IBaseService<Report, Guid>
    {
        Task<List<Report>> GetReportsByFileId(Guid Id);
    }
    public class ReportService : IReportService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ReportService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Report>> All(int PageNumber, int Count) => await _repositoryWrapper.Report.FindAll(PageNumber, Count);
        public Task<Report> Create(Report Report) =>
             _repositoryWrapper.Report.Create(Report);
        public Task<Report> Delete(Guid id) =>
        _repositoryWrapper.Report.Delete(id);

        public Task<Report> FindById(Guid id) =>
        _repositoryWrapper.Report.FindById(id);
        public async Task<List<Report>> GetReportsByFileId(Guid id) =>
        await _repositoryWrapper.Report.GetReportsByFileId(id);
        public async Task<Report> Modify(Guid id, Report Report)
        {
            var ReportModelFromRepo = await _repositoryWrapper.Report.FindById(id);
            if (ReportModelFromRepo == null)
            {
                return null;
            }
            ReportModelFromRepo.Description = Report.Description;
            _repositoryWrapper.Save();
            return Report;
        }

    }
}