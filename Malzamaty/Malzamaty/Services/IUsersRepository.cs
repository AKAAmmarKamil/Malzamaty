using Malzamaty.Model;
using Malzamaty.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Malzamaty.Repository
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User> Authintication(LoginForm login);
        string GetRole(Guid Id);
        bool Match(Guid classes, Guid subjects);
        Task<List<User>> GetAll(int PageNumber, int count);
        Task<User> GetById(Guid Id);
    }
}
