using Malzamaty.Model;
using Malzamaty.Model.Form;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Malzamaty.Repository
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User> Authintication(LoginForm login);
        string GetRole(Guid Id);
        bool Exist(Guid classes, Guid subjects);
    }
}
