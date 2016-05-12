using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Newtonsoft.Json;

namespace OAuth.Resource.Controllers
{
    public class MemberController : ApiController
    {
        [Authorize(Roles = "Admin")]
        public string Get()
        {
            var claims = ((ClaimsIdentity) User.Identity).Claims;

            var roles =
                claims.Where(x => x.Type.Equals(ClaimsIdentity.DefaultRoleClaimType)).Select(x => x.Value).ToList();

            var result = new
                         {
                             UserName = User.Identity.Name,
                             Rolse = roles
            };
            return JsonConvert.SerializeObject(result);
        }
    }
}