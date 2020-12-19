using System;
using Malzamaty.Data;
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
        void Save();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TheContext _repoContext;
        private IUsersRepository _user;
        private ISubjectRepository _subect;
        private ICountryRepository _country;
        private IStageRepository _stage;
        private IClassTypeRepository _classType;
        private IClassRepository _class;
        public ISubjectRepository Subject
        {
            get
            {
                if (_subect == null)
                {
                    _subect = new SubjectRepository(_repoContext);
                }
                return _subect;
            }
        }
        public IUsersRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public ICountryRepository Country
        {
            get
            {
                if (_country == null)
                {
                    _country = new CountryRepository(_repoContext);
                }
                return _country;
            }
        }
        public IStageRepository Stage
        {
            get
            {
                if (_stage == null)
                {
                    _stage = new StageRepository(_repoContext);
                }
                return _stage;
            }
        }
        public IClassTypeRepository ClassType
        {
            get
            {
                if (_classType == null)
                {
                    _classType = new ClassTypeRepository(_repoContext);
                }
                return _classType;
            }
        }
        public IClassRepository Class
        {
            get
            {
                if (_class == null)
                {
                    _class = new ClassRepository(_repoContext);
                }
                return _class;
            }
        }
        public IUsersRepository Users => throw new NotImplementedException();
        public RepositoryWrapper(TheContext repositoryContext)
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
