using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Dao;
using DataAccess.Model;
using Eshop_projekt.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop_projekt.Areas.Admin.Controllers
{
    public class ZpetnaVazbaController : Controller
    {
        // GET: Admin/ZpetnaVazba
        public ActionResult Index()
        {
            ZpetnaVazbaDao zpetnaVazbaDao = new ZpetnaVazbaDao();


            IList<ZpetnaVazba> zpetnaVazba = zpetnaVazbaDao.GetAll();

            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(User.Identity.Name);
            if (uzivatel.Role.Identifikator != "uzivatel")
                return View("IndexPracovnik", zpetnaVazba);
            TempData["success-message-feedback"] = "Zpráva byla zaslána.";
            return View();
            
        }

        
        [HttpPost]
        public ActionResult Add(ZpetnaVazba zpetnaVazba, string zprava )
        {
            string username;
           username = User.Identity.Name;
            
            
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel eshopUzivatel = eshopUzivatelDao.GetByLogin(username);
            zpetnaVazba.eshopUzivatel = eshopUzivatel;
            zpetnaVazba.Zprava = zprava;
            ZpetnaVazbaDao zpetnaVazbaDao = new ZpetnaVazbaDao();
            zpetnaVazbaDao.Create(zpetnaVazba);
            TempData["message-success-hodnoceni"] = "Zpráva byla poslána";

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                ZpetnaVazbaDao zpetnaVazbaDao = new ZpetnaVazbaDao();
                ZpetnaVazba zpetnaVazba = zpetnaVazbaDao.GetById(id);

                zpetnaVazbaDao.Delete(zpetnaVazba);
                TempData["message-success-hodnoceniDelete"] = "Zpráva byla poslána";
            }
            catch (Exception exception)
            {
                throw;
            }

            return RedirectToAction("Index", "ZpetnaVazba");
        }
    }

}