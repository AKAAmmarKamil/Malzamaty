using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IMahallahService : IBaseService<Mahallah, Guid>
    {
        Task<IEnumerable<Mahallah>> GetByDistrict(Guid DistrictId);
    }

    public class MahallahService : IMahallahService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public MahallahService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Mahallah>> All(int PageNumber, int Count) => await _repositoryWrapper.Mahallah.FindAll(PageNumber, Count);
        public Task<Mahallah> Create(Mahallah Mahallah) =>
             _repositoryWrapper.Mahallah.Create(Mahallah);
        public Task<Mahallah> Delete(Guid id) =>
        _repositoryWrapper.Mahallah.Delete(id);

        public Task<Mahallah> FindById(Guid id) =>
        _repositoryWrapper.Mahallah.FindById(id);
        public async Task<IEnumerable<Mahallah>> GetByDistrict(Guid DistrictId) => await _repositoryWrapper.Mahallah.GetByDistrict(DistrictId);
        public async Task<Mahallah> Modify(Guid id, Mahallah Mahallah)
        {
            var MahallahModelFromRepo = await _repositoryWrapper.Mahallah.FindById(id);
            if (MahallahModelFromRepo == null)
            {
                return null;
            }
            MahallahModelFromRepo.Name = Mahallah.Name;
            _repositoryWrapper.Save();
            return Mahallah;
        }

    }
}