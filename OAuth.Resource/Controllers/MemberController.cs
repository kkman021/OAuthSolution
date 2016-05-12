using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace OAuth.Resource.Controllers
{
    public class MemberController : ApiController
    {
        [Authorize]
        public string Get()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;

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