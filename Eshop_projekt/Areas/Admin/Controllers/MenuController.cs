using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop_projekt.Areas.Admin.Controllers
{   [Authorize]
    public class MenuController : Controller
    {
        [ChildActionOnly]
        // GET: Admin/Menu
        public ActionResult Index()
        {
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel eshopUzivatel = eshopUzivatelDao.GetByLogin(User.Identity.Name);
            return View(eshopUzivatel);
        }
    }
}