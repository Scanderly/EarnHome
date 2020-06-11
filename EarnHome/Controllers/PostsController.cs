using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EarnHome.Filters;
using EarnHome.Models;

namespace EarnHome.Controllers
{
    public class PostsController : Controller
    {
        private EarnHomeEntities db = new EarnHomeEntities();

        // GET: Posts
        public ActionResult Index()
        {
            
            var posts = db.Posts.Include(p => p.Category);
            
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Auth]
        public ActionResult Create()
        {
            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Auth]
        
        public ActionResult Create([Bind(Include = "Id,Header,UserId, Desc,Text,CategorId")] Post post)
        {
         
            if (ModelState.IsValid)
            {
                Post newpost = new Post();
                User user = Session["User"] as User;
                newpost.Header = post.Header;
                newpost.Id = post.Id;
                newpost.Text = post.Text;
                newpost.CategorId = post.CategorId;
                newpost.UserId = user.Id;
                db.Posts.Add(newpost);


                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name", post.CategorId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name", post.CategorId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,Desc,Text,Likes,CategorId")] Post post)
        {
            
            if (ModelState.IsValid)
            {
                Post newpost = new Post();
                User user = Session["User"] as User;
                newpost.Header = post.Header;
                newpost.Id = post.Id;
                newpost.Text = post.Text;
                newpost.CategorId = post.CategorId;
                newpost.UserId = user.Id;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategorId = new SelectList(db.Categories, "Id", "Name", post.CategorId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
