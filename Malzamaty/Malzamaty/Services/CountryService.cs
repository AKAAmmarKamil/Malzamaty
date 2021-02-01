using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface ICountryService : IBaseService<Country, Guid>
    {

    }

    public class CountryService : ICountryService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public CountryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Country>> All(int PageNumber, int Count) => await _repositoryWrapper.Country.FindAll(PageNumber, Count);
        public async Task<Country> Create(Country Country) => await
             _repositoryWrapper.Country.Create(Country);
        public async Task<Country> Delete(Guid id) => await
        _repositoryWrapper.Country.Delete(id);

        public async Task<Country> FindById(Guid id) => await
        _repositoryWrapper.Country.FindById(id);

        public async Task<Country> Modify(Guid id, Country Country)
        {
            var CountryModelFromRepo = await _repositoryWrapper.Country.FindById(id);
            if (CountryModelFromRepo == null)
            {
                return null;
            }
            CountryModelFromRepo.Name = Country.Name;
            _repositoryWrapper.Save();
            return Country;
        }

    }
}