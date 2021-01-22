using System;
using Malzamaty.Model;
using Malzamaty.Repository;
using Malzamaty.Services;

namespace Malzamaty
{
    public interface IRepositoryWrapper : IDisposable
    {
        IUsersRepository User { get; }
        ISubjectRepository Subject { get; }
        ICountryRepository Country { get; }
        IStageRepository Stage { get; }
        IClassTypeRepository ClassType { get; }
        IClassRepository Class { get; }
        IInterestRepository Interest { get; }
        IMatchRepository Match { get; }
        IReportRepository Report { get; }
        IFileRepository File { get; }
        IRatingRepository Rating { get; }
        IScheduleRepository Schedule { get; }

        void Save();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MalzamatyContext _repoContext;
        private IUsersRepository _user;
        private ISubjectRepository _subect;
        private ICountryRepository _country;
        private IStageRepository _stage;
        private IClassTypeRepository _classType;
        private IClassRepository _class;
        private IInterestRepository _interest;
        private IMatchRepository _match;
        private IReportRepository _report;
        private IFileRepository _file;
        private IRatingRepository _rating;
        private IScheduleRepository _schedule;
        public ISubjectRepository Subject => _subect ??= new SubjectRepository(_repoContext);
        public IUsersRepository User => _user ??= new UserRepository(_repoContext);

        public ICountryRepository Country =>_country = new CountryRepository(_repoContext);
        public IStageRepository Stage =>_stage ??= new StageRepository(_repoContext);
        public IClassTypeRepository ClassType =>_classType ??= new ClassTypeRepository(_repoContext);
        public IClassRepository Class =>_class ??= new ClassRepository(_repoContext);
        public IInterestRepository Interest =>_interest ??= new InterestsRepository(_repoContext);
        public IMatchRepository Match => _match ??= new MatchRepository(_repoContext);
        public IReportRepository Report=> _report ??= new ReportRepository(_repoContext);
        public IFileRepository File =>_file ??= new FileRepository(_repoContext);
        public IRatingRepository Rating =>  _rating ??= new RatingRepository(_repoContext);
        public IScheduleRepository Schedule=>_schedule ??= new ScheduleRepository(_repoContext);
        public RepositoryWrapper(MalzamatyContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }
    }
}
