using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IInterestRepository :IBaseRepository<Interests>
    {
        Task<List<Interests>> GetInterests(Guid Id);
        Task<bool> CheckIfLast(Guid id);
    }
}
