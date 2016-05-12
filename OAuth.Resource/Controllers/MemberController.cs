using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OAuth.Resource.Controllers
{
    public class MemberController : ApiController
    {
        [Authorize]
        [EnableCors(origins: "http://localhost:9392", headers: "*", methods: "*")]
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