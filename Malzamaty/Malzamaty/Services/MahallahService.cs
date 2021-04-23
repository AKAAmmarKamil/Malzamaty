using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IDistrictService : IBaseService<District, Guid>
    {
        Task<IEnumerable<District>> GetByProvince(Guid ProvinceId);
    }

    public class DistrictService : IDistrictService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public DistrictService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<District>> All(int PageNumber, int Count) => await _repositoryWrapper.District.FindAll(PageNumber, Count);
        public Task<District> Create(District District) =>
             _repositoryWrapper.District.Create(District);
        public Task<District> Delete(Guid id) =>
        _repositoryWrapper.District.Delete(id);

        public Task<District> FindById(Guid id) =>
        _repositoryWrapper.District.FindById(id);
        public async Task<IEnumerable<District>> GetByProvince(Guid ProvinceId) => await _repositoryWrapper.District.GetByProvince(ProvinceId);
        public async Task<District> Modify(Guid id, District District)
        {
            var DistrictModelFromRepo = await _repositoryWrapper.District.FindById(id);
            if (DistrictModelFromRepo == null)
            {
                return null;
            }
            DistrictModelFromRepo.Name = District.Name;
            _repositoryWrapper.Save();
            return District;
        }

    }
}