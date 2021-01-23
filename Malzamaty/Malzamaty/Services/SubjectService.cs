using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface ISubjectService : IBaseService<Subject,Guid>
    {

    }

    public class SubjectService : ISubjectService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public SubjectService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Subject>> All(int PageNumber, int Count) =>await _repositoryWrapper.Subject.FindAll(PageNumber, Count);
        public Task<Subject> Create(Subject subject) =>
             _repositoryWrapper.Subject.Create(subject);
        public Task<Subject> Delete(Guid id)=>
        _repositoryWrapper.Subject.Delete(id);

        public Task<Subject> FindById(Guid id)=>
        _repositoryWrapper.Subject.FindById(id);

        public async Task<Subject> Modify(Guid id, Subject subject)
        {
            var SubjectModelFromRepo =await _repositoryWrapper.Subject.FindById(id);
            if (SubjectModelFromRepo == null)
            {
                return null;
            }
            SubjectModelFromRepo.Name = subject.Name;
            _repositoryWrapper.Save();
            return  subject;
        }

    }
}