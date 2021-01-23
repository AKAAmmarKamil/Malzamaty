using AutoMapper;
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
        protected readonly IMapper _mapper;
        public UserRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
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


    }

}
