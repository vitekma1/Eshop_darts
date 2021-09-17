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
    [Authorize]
    public class ZboziController : Controller
    {
        // GET: Zbozi
        public ActionResult Index(int? page)
        {
            int itemsOnPage = 6;
            int pg = page.HasValue ? page.Value : 1;
            int totalZbozi;
            ZboziDao zboziDao = new ZboziDao();
            IList<Zbozi> zbozi = zboziDao.GetZboziPaged(itemsOnPage,pg, out totalZbozi);

            ViewBag.Pages = (int)Math.Ceiling((double)totalZbozi / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Categories = new ZboziDruhDao().GetAll();
            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(User.Identity.Name);

            if (uzivatel.Role.Identifikator == "uzivatel")
                return View("IndexUzivatel", zbozi);

            return View(zbozi);
        }

        

        public ActionResult Category(int id)
        {
            IList<Zbozi> zbozi = new ZboziDao().GetZboziInCategoryId(id);
            ViewBag.Categories = new ZboziDruhDao().GetAll();
            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(User.Identity.Name);
            if (uzivatel.Role.Identifikator != "uzivatel")
                return View("Index", zbozi);
            return View("IndexUzivatel",zbozi);
        }

        public ActionResult Search(string phrase)
        {
            ZboziDao zboziDao = new ZboziDao();
            IList<Zbozi> zbozi = zboziDao.SearchZbozi(phrase);
            EshopUzivatel uzivatel = new EshopUzivatelDao().GetByLogin(User.Identity.Name);
            if (uzivatel.Role.Identifikator != "uzivatel")
                return View("Index", zbozi);
            return View("IndexUzivatel", zbozi);
        }

        public ActionResult Detail(int id)
        {
            ZboziDao zboziDao = new ZboziDao();
            Zbozi z = zboziDao.GetById(id);


            return View(z);
        }

        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult Create()
        {
            ZboziDruhDao zboziDruhDao = new ZboziDruhDao();
            IList<Zbozi_druh> druhy = zboziDruhDao.GetAll();
            ViewBag.Druhy = druhy;


            return View();
        }
        [HttpPost]
        [Authorize(Roles = "zamestnanec, admin")]

        public ActionResult Add(Zbozi zbozi, HttpPostedFileBase picture, int Druh_id)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);

                            Guid guid = Guid.NewGuid();
                            string obrazek = guid.ToString() + ".jpg";

                            b.Save(Server.MapPath("~/Zbozi/" + obrazek), ImageFormat.Jpeg);

                            smallImage.Dispose();
                            b.Dispose();

                            zbozi.Obrazek = obrazek;
                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/Zbozi/" + picture.FileName));
                        }
                    }
                }
                ZboziDruhDao zboziDruhDao = new ZboziDruhDao();
                Zbozi_druh zbozi_Druh = zboziDruhDao.GetById(Druh_id);
                zbozi.Druh = zbozi_Druh;
                ZboziDao zboziDao = new ZboziDao();
                zboziDao.Create(zbozi);


                TempData["message-success"] = "Kniha byla uspesne pridana";

            }
            else
            {
                return View("Create", zbozi);
            }


            return RedirectToAction("Index");
        }

        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult Edit(int id)
        {
            ZboziDao zboziDao = new ZboziDao();
            ZboziDruhDao zboziDruhDao = new ZboziDruhDao();

            Zbozi z = zboziDao.GetById(id);
            ViewBag.Druhy = zboziDruhDao.GetAll();
            return View(z);

        }

        [Authorize(Roles = "zamestnanec, admin")]
        [HttpPost]
        public ActionResult Update(Zbozi zbozi, HttpPostedFileBase picture, int Druh_id)
        {
            try
            {
                ZboziDao zboziDao = new ZboziDao();
                ZboziDruhDao zboziDruhDao = new ZboziDruhDao();
                Zbozi_druh zbozi_Druh = zboziDruhDao.GetById(Druh_id);
                zbozi.Druh = zbozi_Druh;

                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        Guid guid = Guid.NewGuid();
                        string obrazek = guid.ToString() + ".jpg";

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);


                            b.Save(Server.MapPath("~/Zbozi/" + obrazek), ImageFormat.Jpeg);

                            smallImage.Dispose();
                            b.Dispose();


                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/Zbozi/" + picture.FileName));
                        }
                        zbozi.Obrazek = obrazek;
                    }
                }

                zboziDao.Update(zbozi);
                TempData["message-success"] = "Zbozi" + zbozi.Nazev + " bylo upraveno";
            }
            catch (Exception)
            {
                throw;
            }


            return RedirectToAction("Index", "Zbozi");
        }

        [Authorize(Roles = "zamestnanec, admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                ZboziDao zboziDao = new ZboziDao();
                Zbozi zbozi = zboziDao.GetById(id);
                zbozi.Pocet = 0;
                zbozi.Nazev = "zzzz" + zbozi.Nazev;
                zboziDao.Update(zbozi);
                TempData["message-success"] = "Zboží" + zbozi.Nazev + " bylo vyřazeno";
            }
            catch (Exception exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }
    } }