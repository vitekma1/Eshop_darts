
using DataAccess.Dao;
using DataAccess.Model;
using Eshop_projekt.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop_projekt.Areas.Admin.Controllers
{
    public class RegistraceController : Controller
    {
        public ActionResult Index()
        {
            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(User.Identity.Name);

            if (uzivatel== null)
                return View();
            if (uzivatel.Role.Identifikator == "admin")
                return View("IndexAdmin");
            return View();
        }

        [HttpPost]
        public ActionResult Registrovat(EshopUzivatel eshopUzivatel, string jmeno, string prijmeni, string heslo, string hesloZnova, int psc, string login, string email, string adresa)
        {

            if (jmeno == "" || heslo == "" || hesloZnova == "" || login == "" || prijmeni == "" || email == "" || adresa == "")
            {
                TempData["error-message"] = "Jméno, Příjmení, Login, Heslo, Heslo znova, Adresa a Email jsou povinné údaje!";
                return View("Index", eshopUzivatel);
            }
            if (ModelState.IsValid)
            {
                EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
                EshopUzivatel eshopUzivatelOvereni = eshopUzivatelDao.GetByLogin(login);
                

                if (eshopUzivatelOvereni != null)
                {
                    TempData["error-message"] = "Login " + login + " již existuje, zvolte jiný login.";
                    return View("Index", eshopUzivatel);
                }

                if (!heslo.Equals(hesloZnova))
                {
                    TempData["error-message"] = "Nezadali jste dvakrát stejné heslo. Zadejte ho znova!";
                    return View("Index", eshopUzivatel);
                }

                
                    if(psc>9999 && psc<100000)
                    {
                        eshopUzivatel.PSC = psc;
                    }
                    else
                    {
                        TempData["error-message"] = "Zadali jste PSČ ve špatném tvaru";
                        return View("Index", eshopUzivatel);
                    }


                EshopRoleDao eshopRoleDao = new EshopRoleDao();
                EshopRole eshopRole = eshopRoleDao.GetById(1);
                eshopUzivatel.Role = eshopRole;
                eshopUzivatel.Login = login;
                eshopUzivatel.Jmeno = jmeno;
                eshopUzivatel.Prijmeni = prijmeni;
                string encrPw = PwHelper.EncryptString(eshopUzivatel.Heslo);
                eshopUzivatel.Heslo = encrPw;
                eshopUzivatel.Email = email;
                eshopUzivatel.Adresa = adresa;
                eshopUzivatelDao.Create(eshopUzivatel);
                TempData["success-message-registration"] = "Účet byl vytvořen.";
                return RedirectToAction("Index");
            }
            TempData["error-message"] = "A jéje, něco se pokazilo";
            return View("Index", eshopUzivatel);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult RegistrovatZamestnance(EshopUzivatel eshopUzivatel, string jmeno, string prijmeni, string heslo, string hesloZnova, int psc, string login, string email, string adresa)
        {

            if (jmeno == "" || heslo == "" || hesloZnova == "" || login == "" || prijmeni == "" || email == "" || adresa == "")
            {
                TempData["error-message"] = "Jméno, Příjmení, Login, Heslo, Heslo znova, Adresa a Email jsou povinné údaje!";
                return View("Index", eshopUzivatel);
            }
            if (ModelState.IsValid)
            {
                EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
                EshopUzivatel eshopUzivatelOvereni = eshopUzivatelDao.GetByLogin(login);


                if (eshopUzivatelOvereni != null)
                {
                    TempData["error-message"] = "Login " + login + " již existuje, zvolte jiný login.";
                    return View("Index", eshopUzivatel);
                }

                if (!heslo.Equals(hesloZnova))
                {
                    TempData["error-message"] = "Nezadali jste dvakrát stejné heslo. Zadejte ho znova!";
                    return View("Index", eshopUzivatel);
                }


                if (psc > 9999 && psc < 100000)
                {
                    eshopUzivatel.PSC = psc;
                }
                else
                {
                    TempData["error-message"] = "Zadali jste PSČ ve špatném tvaru";
                    return View("Index", eshopUzivatel);
                }


                EshopRoleDao eshopRoleDao = new EshopRoleDao();
                EshopRole eshopRole = eshopRoleDao.GetById(2);
                eshopUzivatel.Role = eshopRole;
                eshopUzivatel.Login = login;
                eshopUzivatel.Jmeno = jmeno;
                eshopUzivatel.Prijmeni = prijmeni;
                eshopUzivatel.Heslo = heslo;
                eshopUzivatel.Email = email;
                eshopUzivatel.Adresa = adresa;
                eshopUzivatelDao.Create(eshopUzivatel);
                TempData["success-message-registration"] = "Účet byl vytvořen.";
                return RedirectToAction("Index");
            }
            TempData["error-message"] = "A jéje, něco se pokazilo";
            return View("Home", "Index");

        }


    }
}