using Malzamaty.Model;
using Malzamaty.Model.Form;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace Malzamaty.Repository
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User> Authintication(LoginForm login);
        Task<User> GetUserByEmail(string Email);
    }
    public class UserRepository : BaseRepository<User>, IUsersRepository
    {
        private readonly MalzamatyContext _db;

        public UserRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
        public async Task<User> Authintication(LoginForm login) =>
             await _db.Users.Where(x => x.Email == login.EmailAddress && x.Password == login.Password)
                 .FirstOrDefaultAsync();
        public async Task<User> GetUserByEmail(string Email)
        {
           var result= await _db.Users.Where(x => x.Email == Email)
                .FirstOrDefaultAsync();
            if (result==null)
            {
                return null;
            }
            return result;
        }
        public async Task<User> Create(User t)
        {
            await _db.Users.AddAsync(t);
            SaveChanges();
            return t;
        }

        public async Task<User> Delete(Guid id)
        {
            var result = await FindById(id);
            if (result == null) return null;
            _db.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }


    }

}
