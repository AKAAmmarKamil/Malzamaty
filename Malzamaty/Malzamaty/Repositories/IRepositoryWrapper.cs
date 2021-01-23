using System;
using AutoMapper;
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
        private readonly IMapper _mapper;
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
        public ISubjectRepository Subject => _subect ??= new SubjectRepository(_repoContext, _mapper);

        public IUsersRepository User => _user ??= new UserRepository(_repoContext, _mapper);

        public ICountryRepository Country =>_country ??= new CountryRepository(_repoContext, _mapper);
        public IStageRepository Stage =>_stage ??= new StageRepository(_repoContext, _mapper);
        public IClassTypeRepository ClassType =>_classType ??= new ClassTypeRepository(_repoContext, _mapper);
        public IClassRepository Class =>_class ??= new ClassRepository(_repoContext, _mapper);
        public IInterestRepository Interest =>_interest ??= new InterestsRepository(_repoContext, _mapper);
        public IMatchRepository Match => _match ??= new MatchRepository(_repoContext, _mapper);
        public IReportRepository Report=> _report ??= new ReportRepository(_repoContext, _mapper);
        public IFileRepository File =>_file ??= new FileRepository(_repoContext, _mapper);
        public IRatingRepository Rating =>  _rating ??= new RatingRepository(_repoContext, _mapper);
        public IScheduleRepository Schedule=>_schedule ??= new ScheduleRepository(_repoContext, _mapper);
        public RepositoryWrapper(MalzamatyContext repositoryContext,IMapper mapper)
        {
            _repoContext = repositoryContext;
            _mapper = mapper;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }

        public void Dispose()
        {
            _repoContext.Dispose();
        }
    }
}
