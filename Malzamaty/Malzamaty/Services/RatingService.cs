using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IRatingService : IBaseService<Rating, Guid>
    {
        Task<List<Rating>> GetRatingByFile(Guid Id);
    }

    public class RatingService : IRatingService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public RatingService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Rating>> All(int PageNumber, int Count) => await _repositoryWrapper.Rating.FindAll(PageNumber, Count);
        public Task<Rating> Create(Rating Rating) =>
             _repositoryWrapper.Rating.Create(Rating);
        public Task<Rating> Delete(Guid id) =>
        _repositoryWrapper.Rating.Delete(id);

        public Task<Rating> FindById(Guid id) =>
        _repositoryWrapper.Rating.FindById(id);

        public async Task<List<Rating>> GetRatingByFile(Guid Id)
        {
            var File = await _repositoryWrapper.File.FindById(Id);
            if (File != null)
                return await _repositoryWrapper.Rating.GetRatingByFile(Id);
            else return null;
        }
        public async Task<Rating> Modify(Guid id, Rating Rating)
        {
            var RatingModelFromRepo = await _repositoryWrapper.Rating.FindById(id);
            if (RatingModelFromRepo == null)
            {
                return null;
            }
            RatingModelFromRepo.Comment = Rating.Comment;
            RatingModelFromRepo.Rate = Rating.Rate;
            _repositoryWrapper.Save();
            return Rating;
        }

    }
}