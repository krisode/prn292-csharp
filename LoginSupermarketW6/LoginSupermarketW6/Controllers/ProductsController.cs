using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoginSupermarketW6.Models;

namespace LoginSupermarketW6.Controllers
{
    public class ProductsController : Controller
    {
        private SupermarketEntities db = new SupermarketEntities();

        // GET: Products
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string id)
        {
            string searchString = id;
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            products = products.Where(s => s.Status == 0);
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProName.Contains(searchString));
            }
            return View(products.ToList());
            //var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            //return View(products.ToList());
        }

        // GET: Products
        public ActionResult IndexHome(string id)
        {
            string searchString = id;
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            products = products.Where(s => s.Status == 0);
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProName.Contains(searchString));
            }
            return View(products.ToList());
            //var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            //return View(products.ToList());
        }


        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "User")]
        public ActionResult DetailsHome(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName");
            ViewBag.SupID = new SelectList(db.Suppliers, "SupID", "SupName");
            ViewBag.Status = new SelectList(db.Status, "StatusID", "Description");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ProID,ProName,QuantityPerUnit,UnitPrice,UnitInStock,UnitOnOrder,Status,Image,CatID,SupID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", product.CatID);
            ViewBag.SupID = new SelectList(db.Suppliers, "SupID", "SupName", product.SupID);
            ViewBag.Status = new SelectList(db.Status, "StatusID", "Description", product.Status);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", product.CatID);
            ViewBag.SupID = new SelectList(db.Suppliers, "SupID", "SupName", product.SupID);
            ViewBag.Status = new SelectList(db.Status, "StatusID", "Description", product.Status);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ProID,ProName,QuantityPerUnit,UnitPrice,UnitInStock,UnitOnOrder,Status,Image,CatID,SupID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", product.CatID);
            ViewBag.SupID = new SelectList(db.Suppliers, "SupID", "SupName", product.SupID);
            ViewBag.Status = new SelectList(db.Status, "StatusID", "Description", product.Status);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            product.Status = 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
