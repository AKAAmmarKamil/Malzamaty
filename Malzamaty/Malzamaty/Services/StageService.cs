using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface IStageService : IBaseService<Stage,Guid>
    {

    }

    public class StageService : IStageService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public StageService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Stage>> All(int PageNumber, int Count) =>await _repositoryWrapper.Stage.FindAll(PageNumber, Count);
        public async Task<Stage> Create(Stage Stage) =>await
             _repositoryWrapper.Stage.Create(Stage);
        public async Task<Stage> Delete(Guid id)=> await
        _repositoryWrapper.Stage.Delete(id);

        public async Task<Stage> FindById(Guid id)=> await
        _repositoryWrapper.Stage.FindById(id);

        public async Task<Stage> Modify(Guid id, Stage Stage)
        {
            var StageModelFromRepo =await _repositoryWrapper.Stage.FindById(id);
            if (StageModelFromRepo == null)
            {
                return null;
            }
            StageModelFromRepo.Name = Stage.Name;
            _repositoryWrapper.Save();
            return  Stage;
        }

    }
}