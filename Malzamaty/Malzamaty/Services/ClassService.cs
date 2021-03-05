using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IClassService : IBaseService<Class, Guid>
    {
        Task<IEnumerable<Class>> AllInCountry(Guid Country);
    }

    public class ClassService : IClassService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ClassService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Class>> AllInCountry(Guid Country) => await _repositoryWrapper.Class.AllInCountry(Country);

        public Task<IEnumerable<Class>> All(int PageNumber, int Count)
        {
            throw new NotImplementedException();
        }

        public async Task<Class> Create(Class Class) => await
             _repositoryWrapper.Class.Create(Class);
        public async Task<Class> Delete(Guid id) => await
        _repositoryWrapper.Class.Delete(id);

        public async Task<Class> FindById(Guid id) => await
        _repositoryWrapper.Class.FindById(id);

        public async Task<Class> Modify(Guid id, Class Class)
        {
            var ClassModelFromRepo = await _repositoryWrapper.Class.FindById(id);
            if (ClassModelFromRepo == null)
            {
                return null;
            }
            ClassModelFromRepo.Name = Class.Name;
            _repositoryWrapper.Save();
            return Class;
        }

    }
}