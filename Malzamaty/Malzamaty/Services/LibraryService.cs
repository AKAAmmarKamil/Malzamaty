using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface ILibraryService : IBaseService<Library, Guid>
    {
        Task<IEnumerable<Library>> FindAll(int PageNumber, int Count);
    }

    public class LibraryService : ILibraryService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public LibraryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Library>> All(int PageNumber, int Count) => await _repositoryWrapper.Library.FindAll(PageNumber, Count);
        public Task<Library> Create(Library Library) =>
             _repositoryWrapper.Library.Create(Library);
        public Task<Library> Delete(Guid id) =>
        _repositoryWrapper.Library.Delete(id);

        public Task<Library> FindById(Guid id) =>
        _repositoryWrapper.Library.FindById(id);
        public async Task<IEnumerable<Library>> FindAll(int PageNumber, int Count) => await _repositoryWrapper.Library.FindAll(PageNumber,Count);
        public async Task<Library> Modify(Guid id, Library Library)
        {
            var LibraryModelFromRepo = await _repositoryWrapper.Library.FindById(id);
            if (LibraryModelFromRepo == null)
            {
                return null;
            }
            LibraryModelFromRepo.Name = Library.Name;
            _repositoryWrapper.Save();
            return Library;
        }

    }
}