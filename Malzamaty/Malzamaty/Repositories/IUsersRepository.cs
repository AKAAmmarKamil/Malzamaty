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
        string GetRole(Guid Id);
    }
    public class UserRepository : BaseRepository<User>, IUsersRepository
    {
        private readonly MalzamatyContext _db;

        public UserRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
        public async Task<User> Authintication(LoginForm login) =>
             await _db.Users.Include(x=>x.Roles).Where(x => x.Email == login.EmailAddress && x.Password == login.Password)
                 .FirstOrDefaultAsync();

        public async Task<IEnumerable<User>> FindAll(int PageNumber, int count) => await _db.Users.Include(x => x.Roles).Skip((PageNumber - 1) * count).Take(count).ToListAsync();
        public async Task<User> Create(User t)
        {
            await _db.Users.AddAsync(t);
            SaveChanges();
            return t;
        }
        public string GetRole(Guid Id)
        {
            return _db.Roles.Where(x => x.Id == Id).Select(x => x.Role).FirstOrDefault();
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

        public async Task<User> FindById(Guid id)
        {
            var Result = await _db.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.ID == id);
            if (Result == null) return null;
            return Result;
        }


    }

}
