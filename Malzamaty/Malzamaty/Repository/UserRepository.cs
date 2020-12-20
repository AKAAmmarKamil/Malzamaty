using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Malzamaty.Model;
using Malzamaty.Repository;
using Malzamaty.Model.Form;
using Malzamaty.Dto;

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

        public async Task<IEnumerable<UserReadDto>> FindAll(int PageNumber, int count)
        {
            var User= await _db.Users.Skip((PageNumber - 1) * count).Take(count).ToListAsync();
            var Interests = _db.Interests.Include(x=>x.User).ThenInclude(x=>x.Roles).Where(x => x.U_ID == User[0].ID).ToListAsync();
            for (int i = 0; i < User.Count; i++)
            {
                Interests = _db.Interests.Include(x => x.User).ThenInclude(x => x.Roles).Where(x => x.U_ID == User[i].ID).ToListAsync();
            }
            var UserReadDto = new UserReadDto();
            UserReadDto.ID=User
            return Interests;
        }
        public bool Match(Guid classes,Guid subjects)
        {
            var Match= _db.Matches.Where(x => x.C_ID == classes && x.Su_ID == subjects).FirstOrDefault();
              if (Match == null)
            return false;
            else return true;       
        }
        public async Task<User> Create(User t)
        {
            await _db.Users.AddAsync(t);
            await _db.SaveChangesAsync();
            return t;
        }
        public string GetRole(Guid Id)
        {
           return _db.Roles.Where(x=>x.Id==Id).Select(x=>x.Role).FirstOrDefault();
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
          var result=  await _db.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.ID==id);
            return result;
        }
    }
}
