using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;

namespace work_01.Controllers
{
    public class HomeController : Controller
    {
        private ToyStoreDbContext db = new ToyStoreDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
    }
}