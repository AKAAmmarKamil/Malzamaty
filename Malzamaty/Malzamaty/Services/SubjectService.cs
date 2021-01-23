using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Utils;
namespace Malzamaty.Services
{
    public interface ISubjectService : IBaseService<Subject,Guid>
    {

    }

    public class SubjectService : ISubjectService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public SubjectService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public Task<IEnumerable<Subject>> All(int PageNumber,int Count) =>
             _repositoryWrapper.Subject.FindAll(PageNumber,Count);
        public Task<Subject> Create(Subject subject) =>
             _repositoryWrapper.Subject.Create(subject);
        public Task<Subject> Delete(Guid id)=>
        _repositoryWrapper.Subject.Delete(id);

        public Task<Subject> FindById(Guid id)=>
        _repositoryWrapper.Subject.FindById(id);

        public async Task<Subject> Modify(Guid id, Subject subjectWriteDto)
        {
            var SubjectModelFromRepo = _repositoryWrapper.Subject.FindById(id);
            if (SubjectModelFromRepo == null)
            {
                return null;
            }
            SubjectModelFromRepo.Result.Name = subjectWriteDto.Name;
            _repositoryWrapper.Save();
            return  subjectWriteDto;
        }

    }
}