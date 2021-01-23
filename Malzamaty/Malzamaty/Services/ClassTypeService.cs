using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface IClassTypeService : IBaseService<ClassType,Guid>
    {

    }

    public class ClassTypeService : IClassTypeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ClassTypeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<ClassType>> All(int PageNumber, int Count) =>await _repositoryWrapper.ClassType.FindAll(PageNumber, Count);
        public async Task<ClassType> Create(ClassType ClassType) =>await
             _repositoryWrapper.ClassType.Create(ClassType);
        public async Task<ClassType> Delete(Guid id)=> await
        _repositoryWrapper.ClassType.Delete(id);

        public async Task<ClassType> FindById(Guid id)=> await
        _repositoryWrapper.ClassType.FindById(id);

        public async Task<ClassType> Modify(Guid id, ClassType ClassType)
        {
            var ClassTypeModelFromRepo =await _repositoryWrapper.ClassType.FindById(id);
            if (ClassTypeModelFromRepo == null)
            {
                return null;
            }
            ClassTypeModelFromRepo.Name = ClassType.Name;
            _repositoryWrapper.Save();
            return  ClassType;
        }

    }
}