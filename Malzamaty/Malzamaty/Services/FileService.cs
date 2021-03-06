using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IFileService : IBaseService<File, Guid>
    {
        Task<bool> IsExist(string FilePath);
        Task<List<string>> GetYears();
        Task<List<File>> TopRating(Guid Id, bool WithReports);
        Task<File> GetAppropriateFile(Guid Id);
        Task<List<File>> MostDownloaded(Guid Id, bool WithReports);
        Task<List<File>> NewFiles(Guid Id, bool WithReports);
        Task<List<File>> RelatedFiles(Guid Id);
        Task<File> ModifyDownloadCount(Guid id);
        Task<List<File>> GetByName(string FileName);
    }

    public class FileService : IFileService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public FileService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<File>> All(int PageNumber, int Count) => await _repositoryWrapper.File.FindAll(PageNumber, Count);
        public async Task<File> Create(File File) => await
             _repositoryWrapper.File.Create(File);
        public async Task<File> Delete(Guid id) => await
        _repositoryWrapper.File.Delete(id);

        public async Task<File> FindById(Guid id) => await
        _repositoryWrapper.File.FindById(id);

        public Task<bool> IsExist(string FilePath) =>
        _repositoryWrapper.File.IsExist(FilePath);

        public async Task<File> Modify(Guid id, File File)
        {
            var FileModelFromRepo = await _repositoryWrapper.File.FindById(id);
            if (FileModelFromRepo == null)
            {
                return null;
            }
            FileModelFromRepo.Description = File.Description;
            FileModelFromRepo.Author = File.Author;
            FileModelFromRepo.Type = File.Type;
            _repositoryWrapper.Save();
            return File;
        }
        public async Task<File> ModifyDownloadCount(Guid id)
        {
            var FileModelFromRepo = await _repositoryWrapper.File.FindById(id);
            if (FileModelFromRepo == null)
            {
                return null;
            }
            FileModelFromRepo.DownloadCount = Convert.ToInt32(FileModelFromRepo.DownloadCount) + 1;
            _repositoryWrapper.Save();
            return FileModelFromRepo;
        }
        public async Task<List<File>> MostDownloaded(Guid Id, bool WithReports) => await
        _repositoryWrapper.File.MostDownloaded(Id, WithReports);

        public async Task<List<File>> NewFiles(Guid Id, bool WithReports) => await
        _repositoryWrapper.File.NewFiles(Id, WithReports);

        public async Task<List<File>> RelatedFiles(Guid Id) => await
        _repositoryWrapper.File.RelatedFiles(Id);

        public async Task<List<File>> TopRating(Guid Id, bool WithReports) => await
        _repositoryWrapper.File.TopRating(Id, WithReports);
        public async Task<File> GetAppropriateFile(Guid Id) => await
       _repositoryWrapper.File.GetAppropriateFile(Id);

        public Task<List<File>> GetByName(string FileName)=>
            _repositoryWrapper.File.GetByName(FileName);

        public async Task<List<string>> GetYears()
        {
            var CurrentYear = DateTimeOffset.UtcNow.Year;
            var Years = new List<string>();
            for (int i = CurrentYear; i >= 1950; i--)
            {
               Years.Add(i.ToString());
            }
            return  Years;
        }
    }
}