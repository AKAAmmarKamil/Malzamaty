using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface IInterestService : IBaseService<Interests,Guid>
    {
        Task<bool> CheckIfLast(Guid id);
        Task<Interests> FindByUser(Guid Id);
        Task<List<Interests>> GetInterests(Guid Id);
        Task<Interests> ModifyBySchedule(Guid id, Guid Subject);
    }

    public class InterestService : IInterestService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public InterestService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Interests>> All(int PageNumber, int Count) =>await _repositoryWrapper.Interest.FindAll(PageNumber, Count);

        public Task<bool> CheckIfLast(Guid id)=>
         _repositoryWrapper.Interest.CheckIfLast(id);

        public async Task<Interests> Create(Interests Interest) =>await
             _repositoryWrapper.Interest.Create(Interest);
        public async Task<Interests> Delete(Guid id)=> await
        _repositoryWrapper.Interest.Delete(id);

        public async Task<Interests> FindById(Guid id)=> await
        _repositoryWrapper.Interest.FindById(id);

        public Task<Interests> FindByUser(Guid Id)=>
        _repositoryWrapper.Interest.FindByUser(Id);

        public Task<List<Interests>> GetInterests(Guid Id) =>
        _repositoryWrapper.Interest.GetInterests(Id);

        public async Task<Interests> Modify(Guid id, Interests Interest)
        {
            var InterestModelFromRepo =await _repositoryWrapper.Interest.FindById(id);
            if (InterestModelFromRepo == null)
            {
                return null;
            }
            InterestModelFromRepo.SubjectID = Interest.SubjectID;
            InterestModelFromRepo.ClassID = Interest.ClassID;
            _repositoryWrapper.Save();
            return  Interest;
        }

        public async Task<Interests> ModifyBySchedule(Guid id, Guid Subject)
        {
            var User = await _repositoryWrapper.User.FindById(id);
            var Interest = await _repositoryWrapper.Interest.FindByUser(id);
            if (User == null)
            {
                return null;
            }
            Interest.SubjectID = Subject;
            _repositoryWrapper.Save();
            return Interest;
        }
    }
}