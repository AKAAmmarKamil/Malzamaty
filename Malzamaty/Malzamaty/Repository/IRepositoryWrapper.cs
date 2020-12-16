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
        void Save();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TheContext _repoContext;
        private IUsersRepository _user;
        private ISubjectRepository _subect;
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
