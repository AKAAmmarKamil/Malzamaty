using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Malzamaty.Model;
using Malzamaty.Repository;
using Malzamaty.Model.Form;

namespace Malzamaty.Data
{
    public class UserRepository : BaseRepository<User>, IUsersRepository
    {
        private readonly TheContext _db;

        public UserRepository(TheContext context) : base(context)
        {
            _db = context;
        }
        public async Task<User> Authintication(LoginForm login) =>
             await _db.Users.Where(x => x.UserName == login.Username && x.Password==login.Password)
                 .FirstOrDefaultAsync();

        public async Task<List<User>> FindAll(int PageNumber, int count)
        {
            return await _db.Set<User>().Skip(PageNumber * count).Take(count).ToListAsync();
        }

        public async Task<User> Create(User t)
        {
            await _db.Set<User>().AddAsync(t);
            await _db.SaveChangesAsync();
            return t;
        }

        public async Task<User> Update(User entity)
        {
            _db.Set<User>().Update(entity);
            return entity;
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

        public async Task<User> FindById(Guid id) => 
            await _db.Set<User>()
                .FirstOrDefaultAsync(x => x.ID.Equals(id));
    }
}
