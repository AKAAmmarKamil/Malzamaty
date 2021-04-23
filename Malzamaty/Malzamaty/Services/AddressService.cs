using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IAddressService : IBaseService<Address, Guid>
    {
        Task<IEnumerable<Address>> GetAll();
    }

    public class AddressService : IAddressService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AddressService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Address>> All(int PageNumber, int Count) => await _repositoryWrapper.Address.FindAll(PageNumber, Count);
        public Task<Address> Create(Address Address) =>
             _repositoryWrapper.Address.Create(Address);
        public Task<Address> Delete(Guid id) =>
        _repositoryWrapper.Address.Delete(id);

        public Task<Address> FindById(Guid id) =>
        _repositoryWrapper.Address.FindById(id);
        public async Task<IEnumerable<Address>> GetAll() => await _repositoryWrapper.Address.GetAll();
        public async Task<Address> Modify(Guid id, Address Address)
        {
            var AddressModelFromRepo = await _repositoryWrapper.Address.FindById(id);
            if (AddressModelFromRepo == null)
            {
                return null;
            }
            AddressModelFromRepo.ProvinceID = Address.ProvinceID;
            AddressModelFromRepo.DistrictID = Address.DistrictID;
            AddressModelFromRepo.MahallahID = Address.MahallahID;
            AddressModelFromRepo.Details = Address.Details;
            _repositoryWrapper.Save();
            return Address;
        }

    }
}