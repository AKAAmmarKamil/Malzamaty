using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IProvinceService : IBaseService<Province, Guid>
    {
        Task<IEnumerable<Province>> GetByCountry(Guid CountryId);
    }

    public class ProvinceService : IProvinceService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ProvinceService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Province>> All(int PageNumber, int Count) => await _repositoryWrapper.Province.FindAll(PageNumber, Count);
        public Task<Province> Create(Province Province) =>
             _repositoryWrapper.Province.Create(Province);
        public Task<Province> Delete(Guid id) =>
        _repositoryWrapper.Province.Delete(id);

        public Task<Province> FindById(Guid id) =>
        _repositoryWrapper.Province.FindById(id);
        public async Task<IEnumerable<Province>> GetByCountry(Guid CountryId) => await _repositoryWrapper.Province.GetByCountry(CountryId);
        public async Task<Province> Modify(Guid id, Province Province)
        {
            var ProvinceModelFromRepo = await _repositoryWrapper.Province.FindById(id);
            if (ProvinceModelFromRepo == null)
            {
                return null;
            }
            ProvinceModelFromRepo.Name = Province.Name;
            _repositoryWrapper.Save();
            return Province;
        }

    }
}