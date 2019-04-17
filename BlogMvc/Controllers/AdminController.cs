using BlogMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMvc.Controllers
{
    public class AdminController : Controller
    {
        Context db = new Context();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            ViewBag.Makalesayisi = db.Makaleler.Count();
            ViewBag.Yorumsayisi = db.Yorumlar.Count();
            ViewBag.Kategorisayisi = db.Kategoriler.Count();
            ViewBag.UyeSayisi = db.Uyeler.Count();
            return View();
        }
	}
}