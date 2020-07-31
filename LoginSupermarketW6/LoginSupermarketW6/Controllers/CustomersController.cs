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
   
    public class CustomersController : Controller
    {
        private SupermarketEntities db = new SupermarketEntities();

        // GET: Customers
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Status1);
            customers = customers.Where(c => c.Status == 0);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "User,Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var k = db.AspNetUsers.FirstOrDefault(u => u.Id.Equals(id));
            ViewBag.Role = k.AspNetRoles.FirstOrDefault().Name;
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Edit([Bind(Include = "CusID,Address,PhoneNo,CusName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Manage");
            }
            return View(customer);
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
