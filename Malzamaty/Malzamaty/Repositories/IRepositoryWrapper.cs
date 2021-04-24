using AutoMapper;
using Malzamaty.Repository;
using Malzamaty.Services;
using System;

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
        IAddressRepository Address { get; }
        IProvinceRepository Province { get; }
        IDistrictRepository District { get; }
        IMahallahRepository Mahallah { get; }
        ILibraryRepository Library { get; }
        IOrderRepository Order { get; }
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
        private IAddressRepository _address;
        private IProvinceRepository _province;
        private IDistrictRepository _district;
        private IMahallahRepository _mahallah;
        private ILibraryRepository _library;
        private IOrderRepository _order;
        public ISubjectRepository Subject => _subect ??= new SubjectRepository(_repoContext, _mapper);

        public IUsersRepository User => _user ??= new UserRepository(_repoContext, _mapper);

        public ICountryRepository Country => _country ??= new CountryRepository(_repoContext, _mapper);
        public IStageRepository Stage => _stage ??= new StageRepository(_repoContext, _mapper);
        public IClassTypeRepository ClassType => _classType ??= new ClassTypeRepository(_repoContext, _mapper);
        public IClassRepository Class => _class ??= new ClassRepository(_repoContext, _mapper);
        public IInterestRepository Interest => _interest ??= new InterestsRepository(_repoContext, _mapper);
        public IMatchRepository Match => _match ??= new MatchRepository(_repoContext, _mapper);
        public IReportRepository Report => _report ??= new ReportRepository(_repoContext, _mapper);
        public IFileRepository File => _file ??= new FileRepository(_repoContext, _mapper);
        public IRatingRepository Rating => _rating ??= new RatingRepository(_repoContext, _mapper);
        public IScheduleRepository Schedule => _schedule ??= new ScheduleRepository(_repoContext, _mapper);
        public IAddressRepository Address => _address ??= new AddressRepository(_repoContext, _mapper);
        public IProvinceRepository Province => _province ??= new ProvinceRepository(_repoContext, _mapper);
        public IDistrictRepository District => _district ??= new DistrictRepository(_repoContext, _mapper);
        public IMahallahRepository Mahallah => _mahallah ??= new MahallahRepository(_repoContext, _mapper);
        public ILibraryRepository Library => _library ??= new LibraryRepository(_repoContext, _mapper);
        public IOrderRepository Order => _order ??= new OrderRepository(_repoContext, _mapper);
        public RepositoryWrapper(MalzamatyContext repositoryContext, IMapper mapper)
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
