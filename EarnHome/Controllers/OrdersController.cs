﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EarnHome.Models;

namespace EarnHome.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EarnHomeEntities db = new EarnHomeEntities();

        // GET: Orders
        public ActionResult Index(Category category, int?id)
        {
            User user = Session["User"] as User;
            List<Category> categories = db.Categories.OrderByDescending(c => c.Id).ToList();
            ViewBag.Category = categories;
            Category cat = categories.FirstOrDefault(c => c.Id == category.Id);
          
                if (cat == null)
                {
                    return View(db.Orders.OrderByDescending(o => o.Date).ToList());
                }
                else if (cat.Id == id)
                {
                    List<Order> orders = db.Orders.Where(o => o.CategorId == cat.Id).ToList();
                    return View(orders.ToList());
                }
          

            return View();


            //var orders = db.Orders.Include(o => o.Category).Include(o => o.User).Include(o => o.User1).Include(o => o.Post).OrderByDescending(o => o.Date);
            
           
        }
       
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create()
        {
            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            ViewBag.ExecuterId = new SelectList(db.Users, "Id", "Fullname");
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Header");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Status,PostId,UserId,ExecuterId,CategorId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name", order.CategorId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", order.UserId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Header", order.PostId);
            return View(order);
        }
        public ActionResult SendNote()
        {
            return View();
        }
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name", order.CategorId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", order.UserId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Header", order.PostId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status,PostId,UserId,ExecuterId,CategorId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name", order.CategorId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", order.UserId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Header", order.PostId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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
