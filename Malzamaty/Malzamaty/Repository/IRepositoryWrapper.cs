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
        void Save();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TheContext _repoContext;
        private IUsersRepository _user;
        private ISubjectRepository _subect;
        private ICountryRepository _country;
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
