using System;
using System.Collections;
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
    public class OrdersController : Controller
    {
        private SupermarketEntities db = new SupermarketEntities();

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.OrderStatu);
            ViewBag.OderDetailList = db.OderDetails.ToList();

            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CusID = new SelectList(db.Customers, "CusID", "Address");
            ViewBag.Status = new SelectList(db.OrderStatus, "StatusID", "Description");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "OrderID,OrderDate,RequiredDate,ShippedDate,ShipVia,ShipAddress,CusID,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CusID = new SelectList(db.Customers, "CusID", "Address", order.CusID);
            ViewBag.Status = new SelectList(db.OrderStatus, "StatusID", "Description", order.Status);
            return View(order);
        }

        //// GET: Orders/Edit/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CusID = new SelectList(db.Customers, "CusID", "Address", order.CusID);
        //    ViewBag.Status = new SelectList(db.OrderStatus, "StatusID", "Description", order.Status);
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        //public ActionResult Edit([Bind(Include = "OrderID,OrderDate,RequiredDate,ShippedDate,ShipVia,ShipAddress,CusID,Status")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CusID = new SelectList(db.Customers, "CusID", "Address", order.CusID);
        //    ViewBag.Status = new SelectList(db.OrderStatus, "StatusID", "Description", order.Status);
        //    return View(order);
        //}

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
