using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using EarnHome.Models;

namespace EarnHome.Controllers
{
    
    public class HomeController : Controller
    {
        EarnHomeEntities db = new EarnHomeEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Homepage()
        {
            IEnumerable<Post> post = db.Posts;
            ViewBag.Posts = post;
            return View();
        } 
        //public ActionResult HashPassword()
        //{
        //    return Content(Crypto.HashPassword("10041989"));
        //}
        [HttpPost]
        public ActionResult Login(User user)
        {
           
            if (user.Email == null||user.Password==null)
            {
                Session["LoginError"] = "Email və ya şifrə bos ola bilmez";
                return HttpNotFound();
            }
           
            User logined = db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (logined != null)
            {
                if (Crypto.VerifyHashedPassword(logined.Password, user.Password))
                {  
                    Session["Login"] = true;
                    Session["User"] = logined;

                } 
                if (logined.IsAdmin==true)
                {
                    return RedirectToAction("index", "admin", new { Area = "Admin" })/*("homepage", "home")*/;
                }

            }
            return RedirectToAction("homepage","home",new {Area="" });
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User newuser)
        {
            if (newuser.Email == null || newuser.Password == null)
            {
                Session["RegisterError"] = "Email və şifrə boş ola bilməz";
                return HttpNotFound();
            }
            User user = db.Users.FirstOrDefault(u => u.Email == newuser.Email);
            if(user!=null)
            {
                Session["RegisterError"] = "Bu istifadəçi artıq mövcuddur";
                return HttpNotFound();
            }
            user = new User();
            user.Password = Crypto.HashPassword(newuser.Password);
            user.Email = newuser.Email;
            user.Fullname = newuser.Fullname;
            db.Users.Add(user);
            db.SaveChanges();
            Session["Register"] = true;
            return RedirectToAction("index", "home", new { Area = "" });
        }
        public ActionResult Logout()
        {
            Session["Login"] = false;
            Session["User"] = null;
            return RedirectToAction("homepage", "home", new { Area = "" });

        }
    }

}