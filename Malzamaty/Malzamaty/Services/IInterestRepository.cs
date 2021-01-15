using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IInterestRepository :IBaseRepository<Interests>
    {
        Task<List<Interests>> GetInterests(Guid Id);
        Task<List<Interests>> GetAll(int PageNumber, int count);
        Task<bool> CheckIfLast(Guid id);
    }
}
