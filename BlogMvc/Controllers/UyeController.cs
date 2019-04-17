using BlogMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BlogMvc.Controllers
{
    public class UyeController : Controller
    {
        Context db = new Context();
        //
        // GET: /Uye/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Uye uye)
        {

            if (ModelState.IsValid)
            {
                var login = db.Uyeler.Where(u => u.KullanıcıAdı == uye.KullanıcıAdı).SingleOrDefault();
                if (login.KullanıcıAdı == uye.KullanıcıAdı && login.Email == uye.Email && login.Sifre == uye.Sifre)
                {

                    Session["uyeid"] = login.UyeID;
                    Session["kullaniciadi"] = login.KullanıcıAdı;
                    Session["yetkiid"] = login.YetkiID;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            return View();
       }
     
        
        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Uye uye,HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    uye.Foto = "/Uploads/UyeFoto/" + newfoto;﻿
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.YetkiID = 2;
                    db.Uyeler.Add(uye);
                    db.SaveChanges();
                    Session["UyeID"] = uye.UyeID;
                    Session["KullanıcıAdı"] = uye.KullanıcıAdı;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Fotograf", "Fotograf Seciniz");
                }
             
            }
            return View();
        }
	}
}