using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IMatchService : IBaseService<Match, Guid>
    {
        Task<IEnumerable<Match>> FindByClass(Guid Id, Guid Subject);
    }

    public class MatchService : IMatchService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public MatchService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Match>> All(int PageNumber, int Count) => await _repositoryWrapper.Match.FindAll(PageNumber, Count);
        public Task<Match> Create(Match Match) =>
             _repositoryWrapper.Match.Create(Match);
        public Task<Match> Delete(Guid id) =>
        _repositoryWrapper.Match.Delete(id);

        public Task<IEnumerable<Match>> FindByClass(Guid Id, Guid Subject) =>
        _repositoryWrapper.Match.FindByClass(Id, Subject);
        public Task<Match> FindById(Guid id) =>
        _repositoryWrapper.Match.FindById(id);

        public async Task<Match> Modify(Guid id, Match Match)
        {
            var MatchModelFromRepo = await _repositoryWrapper.Match.FindById(id);
            if (MatchModelFromRepo == null)
            {
                return null;
            }
            MatchModelFromRepo.ClassID = Match.ClassID;
            MatchModelFromRepo.SubjectID = Match.SubjectID;
            _repositoryWrapper.Save();
            return Match;
        }

    }
}