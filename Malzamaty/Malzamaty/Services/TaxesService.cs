using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface ITaxesService : IBaseService<Taxes, Guid>
    {
        Task<IEnumerable<Taxes>> GetAll();
        Task<double> GetFinalPrice(Guid FileId);
    }

    public class TaxesService : ITaxesService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public TaxesService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Taxes>> All(int PageNumber, int Count) => await _repositoryWrapper.Taxes.FindAll(PageNumber, Count);
        public Task<Taxes> Create(Taxes Taxes) =>
             _repositoryWrapper.Taxes.Create(Taxes);
        public Task<Taxes> Delete(Guid id) =>
        _repositoryWrapper.Taxes.Delete(id);
        public Task<double> GetFinalPrice(Guid FileId) =>
       _repositoryWrapper.Taxes.GetFinalPrice(FileId);
        public Task<Taxes> FindById(Guid id) =>
        _repositoryWrapper.Taxes.FindById(id);
        public async Task<IEnumerable<Taxes>> GetAll() => await _repositoryWrapper.Taxes.GetAll();
        public async Task<Taxes> Modify(Guid id, Taxes Taxes)
        {
            var TaxesModelFromRepo = await _repositoryWrapper.Taxes.FindById(id);
            if (TaxesModelFromRepo == null)
            {
                return null;
            }
            TaxesModelFromRepo.DeliveryTaxes = Taxes.DeliveryTaxes;
            TaxesModelFromRepo.DeliveryDiscount = Taxes.DeliveryDiscount;
            TaxesModelFromRepo.MalzamatyTaxes = Taxes.MalzamatyTaxes;
            TaxesModelFromRepo.MalzamatyDiscount = Taxes.MalzamatyDiscount;
            _repositoryWrapper.Save();
            return Taxes;
        }

    }
}