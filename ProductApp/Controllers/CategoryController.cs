using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductApp.Controllers
{
    public class CategoryController : Controller
    {
        ProductContext db = new ProductContext();
        
        // GET: Category
        public ActionResult Index()
        {
            var data = db.categories.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int id)
        {
            var category = db.categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        public ActionResult Delete(int id)
        {
            var category = db.categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            db.categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}