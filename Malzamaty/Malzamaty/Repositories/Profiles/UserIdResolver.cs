using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Malzamaty.Repositories.Profiles
{
    public class UserIdResolver : IValueResolver<object, object, string>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserIdResolver(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Resolve(object source, object destination, string destinationMember, ResolutionContext context)
        {
            return _contextAccessor.HttpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();
        }
    }
}
