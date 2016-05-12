﻿using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace OAuth.Server.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;
            if (Request.HttpMethod == "POST")
            {
                var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("isPersistent"));

                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Signin")))
                {
                    authentication.SignIn(
                        new AuthenticationProperties {IsPersistent = isPersistent},
                        new ClaimsIdentity(
                            new[] {new Claim(ClaimsIdentity.DefaultNameClaimType, Request.Form["username"])},
                            "Application"));
                }
            }

            return View();
        }
        
        public ActionResult Logout()
        {
            return View();
        }
    }
}