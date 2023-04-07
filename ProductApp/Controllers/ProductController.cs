using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        ProductContext db = new ProductContext();

        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var products = db.products.Include("Category").ToList();
            var totalCount = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                page = totalPages;
            }

            if (page < 1)
            {
                page = 1;
            }

            var productsPage = products.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(productsPage);
        }

        public ActionResult Create()
        {

            ViewData["Category"] = new SelectList(db.categories.ToList(), "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
          
            if (ModelState.IsValid)
            {
                Product p = new Product();
                p.ProductId = product.ProductId;
                p.ProductName = product.ProductName;
                p.CategoryId = product.CategoryId;
                p.Description = product.Description;
                db.Entry(p).State = System.Data.Entity.EntityState.Added;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            ViewData["Category"] = new SelectList(db.categories.ToList(), "CategoryId", "CategoryName");
            var product = db.products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = db.products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            db.products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}