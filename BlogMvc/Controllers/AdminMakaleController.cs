using BlogMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
namespace BlogMvc.Controllers
{
    public class AdminMakaleController : Controller
    {
        Context db = new Context();
        //
        // GET: /AdminMakale/
        public ActionResult Index()
        {
         
            var makales = db.Makaleler.ToList();
            return View(makales);
        }

        //
        // GET: /AdminMakale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AdminMakale/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdı");
            return View();
        }

        //
        // POST: /AdminMakale/Create
        [HttpPost]
        public ActionResult Create(Makale makale,string etiketler,HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    makale.Foto = "/Uploads/MakaleFoto/" + newfoto;﻿
                    img.Save("~/Uploads/MakaleFoto/" + newfoto);
                   


                }
                if (etiketler != null)
                {
                    string[] etiketdizi = etiketler.Split(',');
                    foreach (var i in etiketdizi)
                    {
                        var yenietiket = new Etiket { EtiketAdı = i };
                        db.Etiketler.Add(yenietiket);
                      
                    }
                }
                makale.UyeID = Convert.ToInt32(Session["uyeid"]);
                makale.Okunma = 1;
                db.Makaleler.Add(makale);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(makale);
        }

        //
        // GET: /AdminMakale/Edit/5
        public ActionResult Edit(int id)
        {
            var makale = db.Makaleler.Where(m => m.MakaleID == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdı", makale.KategoriID);
            return View(makale);
        }

        //
        // POST: /AdminMakale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Makale makale)
        {
            try
            {
                var makales = db.Makaleler.Where(m => m.MakaleID == id).SingleOrDefault();
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(makales.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(makales.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/MakaleFoto/" + newfoto);
                    makales.Foto = "/Uploads/MakaleFoto/" + newfoto;
                    makales.Baslik = makale.Baslik;
                    makales.Icerik = makale.Icerik;
                    makales.KategoriID = makale.KategoriID;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdı", makale.KategoriID);
                return View(makale);
            }
        }

        //
        // GET: /AdminMakale/Delete/5
        public ActionResult Delete(int id)
        {
            var makale = db.Makaleler.Where(m => m.MakaleID == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        //
        // POST: /AdminMakale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var makales = db.Makaleler.Where(m => m.MakaleID == id).SingleOrDefault();
                if (makales == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(makales.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(makales.Foto));
                }
                foreach (var i in makales.Yorumlar.ToList())
                {
                    db.Yorumlar.Remove(i);
                }
               
                db.Makaleler.Remove(makales);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
