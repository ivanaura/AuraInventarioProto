﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using static AuraInventarioProto.App_Start.HashClass;

namespace AuraInventarioProto.Controllers {
    public class LOGINController : Controller {
        private AuraInventarioProtoDBEntities1 db = new AuraInventarioProtoDBEntities1();

        // GET: Index
        public ActionResult Index() {
            return View(db.LOGIN.ToList());
        }

        // GET: LOGIN/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGIN lOGIN = db.LOGIN.Find(id);
            if (lOGIN == null) {
                return HttpNotFound();
            }
            return View(lOGIN);
        }

        // GET: LOGIN/Create
        public ActionResult Create() {
            return View();
        }

        // POST: LOGIN/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RUT,CORREO,PASS")] LOGIN lOGIN) {
            if (ModelState.IsValid) {
                string salt = CreateSalt();
                lOGIN.SALT = salt;
                string pass = lOGIN.PASS + salt;
                lOGIN.PASS = CreateHash(pass);

                //lOGIN.PASS = CreateHash(pass, new SHA256CryptoServiceProvider());


                db.LOGIN.Add(lOGIN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOGIN);
        }



        // GET: LOGIN/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGIN lOGIN = db.LOGIN.Find(id);
            if (lOGIN == null) {
                return HttpNotFound();
            }
            return View(lOGIN);
        }

        // POST: LOGIN/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RUT,CORREO,PASS,SALT")] LOGIN lOGIN) {
            if (ModelState.IsValid) {
                db.Entry(lOGIN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOGIN);
        }

        // GET: LOGIN/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGIN lOGIN = db.LOGIN.Find(id);
            if (lOGIN == null) {
                return HttpNotFound();
            }
            return View(lOGIN);
        }

        // POST: LOGIN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            LOGIN lOGIN = db.LOGIN.Find(id);
            db.LOGIN.Remove(lOGIN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult doesRutExist(string Rut) {
            var user = db.USUARIOS.FirstOrDefault(p => p.RUT == Rut);

            return Json(user);
        }

        [HttpPost]
        public JsonResult doesCorreoExist(string Correo) {
            var email = db.USUARIOS.FirstOrDefault(p => p.CORREO == Correo);

            return Json(email);
        }
    }
}