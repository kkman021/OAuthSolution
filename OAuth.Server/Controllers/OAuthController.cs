using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Constants;

namespace OAuth.Server.Controllers
{
    public class OAuthController : Controller
    {

        public ActionResult Authorize()
        {
            if (Response.StatusCode != 200)
            {
                return View("AuthorizeError");
            }

            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync(OAuthCfg.ApplicationName).Result;
            var identity = ticket?.Identity;

            //使用者尚未登入，導至登入頁面
            if (identity == null)
            {
                authentication.Challenge(OAuthCfg.ApplicationName);
                return new HttpUnauthorizedResult();
            }

            var scopes = (Request.QueryString.Get("scope") ?? "").Split(' ');

            if (Request.HttpMethod == "POST")
            {
                //建立Scope授權
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Grant")))
                {
                    identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType,
                        identity.RoleClaimType);

                    foreach (var scope in scopes)
                    {
                        identity.AddClaim(new Claim("urn:oauth:scope", scope));
                    }

                    //假Role資料
                    identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"));

                    authentication.SignIn(identity);
                }

                //用另外一個身份登入按鈕
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Login")))
                {
                    authentication.SignOut(OAuthCfg.ApplicationName);
                    authentication.Challenge(OAuthCfg.ApplicationName);
                    return new HttpUnauthorizedResult();
                }
            }

            return View();
        }
    }
}