using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EarnHome.Models;

namespace EarnHome.Areas.Executer.Controllers
{
    public class ProfileController : Controller
    {
        private EarnHomeEntities db = new EarnHomeEntities();

        // GET: Executer/Profile
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.UserSkill);
            return View(users.ToList());
        }

        // GET: Executer/Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Executer/Profile/Create
        public ActionResult Create()
        {
            ViewBag.UserSkillsId = new SelectList(db.UserSkills, "Id", "Name");
            return View();
        }

        // POST: Executer/Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fullname,IsAdmin,Email,Password,UserSkillsId,Photo")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserSkillsId = new SelectList(db.UserSkills, "Id", "Name", user.UserSkillsId);
            return View(user);
        }

        // GET: Executer/Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserSkillsId = new SelectList(db.UserSkills, "Id", "Name", user.UserSkillsId);
            return View(user);
        }

        // POST: Executer/Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fullname,IsAdmin,Email,Password,UserSkillsId,Photo")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserSkillsId = new SelectList(db.UserSkills, "Id", "Name", user.UserSkillsId);
            return View(user);
        }

        // GET: Executer/Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Executer/Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
