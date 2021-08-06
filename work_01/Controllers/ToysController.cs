using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using work_01.Models;
using work_01.ViewModels;

namespace work_01.Controllers
{
    [Authorize]
    public class ToysController : Controller
    {
        private ToyStoreDbContext db = new ToyStoreDbContext();
        [AllowAnonymous]
        public ActionResult Index(string search = "")
        {
            var toys = db.Toys.Include("Categories").Select(x => new ToyVM
            {
                Id = x.Id,
                ToysName = x.ToysName,
                cName = x.Categories.cName,
                Price = x.Price,
                StoreDate = x.StoreDate,
                PicturePath = x.PicturePath
            });
            if (!string.IsNullOrEmpty(search))
            {
                toys = toys.Where(x => x.ToysName.ToLower().StartsWith(search.ToLower()));
            }
            return View(toys);
        }
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "cId", "cName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToyVM tvm)
        {
            if (ModelState.IsValid)
            {
                if (tvm.Picture != null)
                {
                    string filepath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(tvm.Picture.FileName));
                    tvm.Picture.SaveAs(Server.MapPath(filepath));

                    Toys toys = new Toys
                    {
                        ToysName = tvm.ToysName,
                        cId = tvm.cId,
                        Price = tvm.Price,
                        StoreDate = tvm.StoreDate,
                        PicturePath = filepath
                    };
                    db.Toys.Add(toys);
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.Categories = new SelectList(db.Categories, "cId", "cName");
            return PartialView("_error");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toys toys = db.Toys.Find(id);
            if (toys == null)
            {
                return HttpNotFound();
            }
            ToyVM tvm = new ToyVM
            {
                Id = toys.Id,
                ToysName = toys.ToysName,
                cId = toys.cId,
                Price = toys.Price,
                StoreDate = toys.StoreDate,
                PicturePath = toys.PicturePath
            };
            ViewBag.Categories = new SelectList(db.Categories, "cId", "cName", tvm.cId);
            return View(tvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ToyVM tvm)
        {
            if (ModelState.IsValid)
            {
                string filepath = tvm.PicturePath;
                if (tvm.Picture != null)
                {
                    filepath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(tvm.Picture.FileName));
                    tvm.Picture.SaveAs(Server.MapPath(filepath));

                    Toys toys = new Toys
                    {
                        Id = tvm.Id,
                        ToysName = tvm.ToysName,
                        cId = tvm.cId,
                        Price = tvm.Price,
                        StoreDate = tvm.StoreDate,
                        PicturePath = filepath
                    };
                    db.Entry(toys).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Toys toys = new Toys
                    {
                        Id = tvm.Id,
                        ToysName = tvm.ToysName,
                        cId = tvm.cId,
                        Price = tvm.Price,
                        StoreDate = tvm.StoreDate,
                        PicturePath = filepath
                    };
                    db.Entry(toys).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Categories = new SelectList(db.Categories, "cId", "cName", tvm.cId);
            return View(tvm);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toys toys = db.Toys.Find(id);
            if (toys == null)
            {
                return HttpNotFound();
            }
            return View(toys);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Toys toys = db.Toys.Find(id);

            string file_name = toys.PicturePath;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            db.Toys.Remove(toys);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}