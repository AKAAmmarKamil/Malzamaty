using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Malzamaty.Controllers {
    public abstract class BaseController : Controller {
        protected string GetClaim(string claimName)
        {
            return (User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(c =>
                string.Equals(c.Type, claimName, StringComparison.CurrentCultureIgnoreCase))?.Value;
        }
    }
}