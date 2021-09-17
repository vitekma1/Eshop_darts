using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Eshop_projekt.Areas.Admin.Controllers
{
    public class ObjednavkaController : Controller
    {
        // GET: Admin/Objednavka
        public ActionResult Index()
        {
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            IList<Objednavka> objednavka = objednavkaDao.GetAll();

            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(User.Identity.Name);
            if (uzivatel.Role.Identifikator != "uzivatel")
                return View("IndexPracovnik", objednavka);
            return View("Index", objednavka);
        }
        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult Sort(string login)
        {
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            IList<Objednavka> objednavka = objednavkaDao.GetAll();

            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(login);
            ViewBag.Login = login;
            return View("IndexSort", objednavka);
        }

        public ActionResult AddObjednavka(Objednavka objednavka, int id)
        {
            ZboziDao zboziDao = new ZboziDao();
            Zbozi zbozi = zboziDao.GetById(id);
            string username;
            username = User.Identity.Name;
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel eshopUzivatel = eshopUzivatelDao.GetByLogin(username);
            objednavka.eshopUzivatel = eshopUzivatel;
            objednavka.zbozi = zbozi;
            objednavka.Cena = zbozi.Cena;
            objednavka.Ks = 1;
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            objednavkaDao.Create(objednavka);
            return RedirectToAction("Index", "Objednavka");
        }
        
        public ActionResult Objednani(int id)
        {
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel eshopUzivatel = eshopUzivatelDao.GetById(id);
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            IList<Objednavka> objednavky = objednavkaDao.GetAll();
            
            foreach (Objednavka objednavka in objednavky)
            {
                if (objednavka.Platnost == 0)
                {
                    if (objednavka.eshopUzivatel.Login == eshopUzivatel.Login)
                {
                    objednavka.Platnost = 1;
                    TempData["success-message-objednavka"] = "Objednávka byla odeslána";

                    objednavkaDao.Update(objednavka);
                }
                }
            }
            return RedirectToAction("Index", "Objednavka");

        }

        public ActionResult Zruseni(int id)
        {
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel eshopUzivatel = eshopUzivatelDao.GetById(id);
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            IList<Objednavka> objednavky = objednavkaDao.GetAll();

            foreach (Objednavka objednavka in objednavky)
            {
                if (objednavka.eshopUzivatel.Login == eshopUzivatel.Login)
                {
                    if(objednavka.Platnost==0){ 
                    objednavka.Platnost = 2;
                    objednavkaDao.Update(objednavka);
                    TempData["success-message-objednavkaZrusena"] = "Objednávka byla zrušena";

                    }
                }

            }
            return RedirectToAction("Index", "Objednavka");

        }
        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult Odeslano(int id)
        {
           

            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            Objednavka objednavka = objednavkaDao.GetById(id);

            
                
                        objednavka.Platnost = 2;
                        objednavkaDao.Update(objednavka);
                  

            
            return RedirectToAction("Index", "Objednavka");

        }

        public ActionResult PridatKs(int id, int ks)
        {


            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            Objednavka objednavka = objednavkaDao.GetById(id);
            objednavka.Ks = ks;
            objednavka.Cena = objednavka.zbozi.Cena*ks;
            objednavkaDao.Update(objednavka);

            return RedirectToAction("Index", "Objednavka");

        }
        public ActionResult Smazat(int id,string uzivatel)
        {

            EshopUzivatel uzivatel1 = new EshopUzivatelDao().GetByLogin(uzivatel);
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            Objednavka objednavka = objednavkaDao.GetById(id);
            if (objednavka.eshopUzivatel.Login == uzivatel1.Login) { 
            objednavkaDao.Delete(objednavka);
            }
            return RedirectToAction("Index", "Objednavka");

        }

        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult OdeslanoVse(string login)
        {

            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            IList<Objednavka> objednavka = objednavkaDao.GetAll();

            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(login);
            ViewBag.Login = login;
            
            foreach (Objednavka objednavka2 in objednavka)
            {

                
                if (objednavka2.eshopUzivatel.Login == login)
                {
                    objednavka2.Platnost = 2;
                    objednavkaDao.Update(objednavka2);

                    }
                
            }
            return View("IndexPracovnik", objednavka);
        }
    }
}