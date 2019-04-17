using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMvc.Models;

namespace BlogMvc.Controllers
{
    public class AdminUyeController : Controller
    {
        private Context db = new Context();

        // GET: /AdminUye/
        public ActionResult Index()
        {
            var uyeler = db.Uyeler.Include(u => u.Yetki);
            return View(uyeler.OrderByDescending(u=>u.UyeID).ToList());
        }

        // GET: /AdminUye/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uyeler.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: /AdminUye/Create
        public ActionResult Create()
        {
            ViewBag.YetkiID = new SelectList(db.Yetkiler, "YetkiID", "YetkiAdı");
            return View();
        }

        // POST: /AdminUye/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UyeID,KullanıcıAdı,Email,Sifre,AdSoyad,YetkiID")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Uyeler.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YetkiID = new SelectList(db.Yetkiler, "YetkiID", "YetkiAdı", uye.YetkiID);
            return View(uye);
        }

        // GET: /AdminUye/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uyeler.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            ViewBag.YetkiID = new SelectList(db.Yetkiler, "YetkiID", "YetkiAdı", uye.YetkiID);
            return View(uye);
        }

        // POST: /AdminUye/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UyeID,KullanıcıAdı,Email,Sifre,AdSoyad,YetkiID")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YetkiID = new SelectList(db.Yetkiler, "YetkiID", "YetkiAdı", uye.YetkiID);
            return View(uye);
        }

        // GET: /AdminUye/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uyeler.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: /AdminUye/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uye uye = db.Uyeler.Find(id);
            db.Uyeler.Remove(uye);
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
