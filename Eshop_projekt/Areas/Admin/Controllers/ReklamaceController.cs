using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop_projekt.Areas.Admin.Controllers
{
    public class ReklamaceController : Controller
    {
        // GET: Admin/Reklamace
        public ActionResult Index()
        {
            ReklamaceDao reklamaceDao = new ReklamaceDao();


            IList<Reklamace> reklamace = reklamaceDao.GetAll();

            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(User.Identity.Name);
            if (uzivatel.Role.Identifikator != "uzivatel")
                return View("IndexPracovnik", reklamace);
            return View();

        }


        [HttpPost]
        public ActionResult Add(Reklamace reklamace, string popis, int id)
        {
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            Objednavka objednavka = objednavkaDao.GetById(id);
            reklamace.objednavka = objednavka;
            string username;
            username = User.Identity.Name;
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel eshopUzivatel = eshopUzivatelDao.GetByLogin(username);
            reklamace.eshopUzivatel = eshopUzivatel;
            reklamace.Popis = popis;
            ReklamaceDao reklamaceDao = new ReklamaceDao();
            reklamaceDao.Create(reklamace);
            TempData["message-success-reklamace"] = "Reklamace byla zaslána";

            return RedirectToAction("Index", "Reklamace");
        }

        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                ReklamaceDao reklamaceDao = new ReklamaceDao();
                Reklamace reklamace = reklamaceDao.GetById(id);
                
                reklamaceDao.Delete(reklamace);
                TempData["message-success-reklamaceDelete"] = "Reklamace byla vymazána";
            }
            catch (Exception exception)
            {
                throw;
            }

            return RedirectToAction("Index", "Reklamace");
        }
    }
}