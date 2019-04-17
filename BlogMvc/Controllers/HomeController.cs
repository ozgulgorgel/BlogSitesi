using BlogMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BlogMvc.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();
        public ActionResult Index(int Page=1)
        {
           var makale = db.Makaleler.OrderByDescending(a => a.MakaleID).ToPagedList(Page, 2);
            return View(makale);
           
        }
        public ActionResult MakaleDetay(int id)
        {
            var makale = db.Makaleler.Where(m => m.MakaleID == id).SingleOrDefault();
            if(makale==null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }
        public ActionResult SonYorumlar()
        {
            return View(db.Yorumlar.OrderByDescending(y => y.YorumID).Take(5));
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult İletisim()
        {
            return View();
        }
        public ActionResult BlogAra(string Ara = null)
        {
            var aranan = db.Makaleler.Where(m => m.Baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(m => m.Tarih));
        }
        public ActionResult KategoriMakale(int id)
        {
            var makaleler = db.Makaleler.Where(x=> x.Kategori.KategoriID == id).ToList();
            return View(makaleler);
        }
        public ActionResult KategoriPartial()
        {
            return PartialView(db.Kategoriler.ToList());
        }
        public JsonResult YorumYap(string yorum, int Makaleid)
        {
            var uyeid = Session["uyeid"];
            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            db.Yorumlar.Add(new Yorum { UyeID = Convert.ToInt32(uyeid), MakaleID = Makaleid, Icerik = yorum, Tarih = DateTime.Now });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorumlar.Where(a => a.YorumID == id).FirstOrDefault();
            var makale = db.Makaleler.Where(m => m.MakaleID == yorum.MakaleID).FirstOrDefault();
            if(yorum.UyeID==Convert.ToInt32(uyeid))
            {
                db.Yorumlar.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("MakaleDetay", "Home", new { id = makale.MakaleID });
            }
            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult OkunmaArttir(int Makaleid)
        {
            var makale = db.Makaleler.Where(m => m.MakaleID == Makaleid).SingleOrDefault();
            makale.Okunma += 1;
            db.SaveChanges();
            return View();
        }
        public ActionResult PopulerMakaleler()
        {
            return View(db.Makaleler.OrderByDescending(m=>m.Okunma).Take(5));
        }
    }
}