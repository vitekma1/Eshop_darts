using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Eshop_projekt.Areas.Admin.Controllers
{
    public class PrihlaseniController : Controller
    {
        // GET: Prihlaseni
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string login, string heslo)
        {
            if (Membership.ValidateUser(login, heslo))
            {
                FormsAuthentication.SetAuthCookie(login, false);
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Login nebo heslo neni spravne";
            return RedirectToAction("Index", "Prihlaseni");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Prihlaseni");
        }
    }
}