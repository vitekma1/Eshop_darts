using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;
using Eshop_projekt.Class;

namespace Eshop_projekt.Controllers
{
    [Authorize]
    public class ZboziController : Controller
    {
        // GET: Zbozi
        public ActionResult Index()
        {
            ZboziDao zboziDao = new ZboziDao();
            IList <Zbozi> zbozi = zboziDao.GetAll();
           
            return View(zbozi);
        }

        public ActionResult Detail(int id)
        {
            ZboziDao zboziDao = new ZboziDao();
            Zbozi z = zboziDao.GetById(id);


            return View(z);
        }

    }
}