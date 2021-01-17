using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Malzamaty.Controllers {
    public abstract class BaseController : Controller {
        protected string GetClaim(string claimName) {
            return (User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(c =>
                string.Equals(c.Type, claimName, StringComparison.CurrentCultureIgnoreCase))?.Value;
        }
    }
}