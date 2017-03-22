﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;

namespace AuraInventarioProto.Controllers
{
    public class UNEController : Controller
    {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: UNE
        public ActionResult Index()
        {
            return View(db.UNE.ToList());
        }

        // GET: UNE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNE uNE = db.UNE.Find(id);
            if (uNE == null)
            {
                return HttpNotFound();
            }
            return View(uNE);
        }

        // GET: UNE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UNE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OBRA,DESCRIPCION")] UNE uNE)
        {
            if (ModelState.IsValid)
            {
                uNE.OBRA = uNE.OBRA.ToUpper();
                db.UNE.Add(uNE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uNE);
        }

        // GET: UNE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNE uNE = db.UNE.Find(id);
            if (uNE == null)
            {
                return HttpNotFound();
            }
            return View(uNE);
        }

        // POST: UNE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OBRA,DESCRIPCION")] UNE uNE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uNE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uNE);
        }

        // GET: UNE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNE uNE = db.UNE.Find(id);
            if (uNE == null)
            {
                return HttpNotFound();
            }
            return View(uNE);
        }

        // POST: UNE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UNE uNE = db.UNE.Find(id);
            db.UNE.Remove(uNE);
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