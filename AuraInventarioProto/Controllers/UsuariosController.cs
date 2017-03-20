using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using AuraInventarioProto.App_Start;

namespace AuraInventarioProto.Controllers {
    //    [Authorize]
    [SessionExpire]
    public class USUARIOSController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: USUARIOS
        //[AllowAnonymous]
        public ActionResult Index() {
            return View(db.USUARIOS.ToList());
        }

        // GET: USUARIOS/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null) {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Create
        public ActionResult Create() {
            return View();
        }

        // POST: USUARIOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RUT,NOMBRE_C,CORREO,UNE")] USUARIOS uSUARIOS) {
            if (ModelState.IsValid) {
                if (db.USUARIOS.Any(o => o.RUT == uSUARIOS.RUT)) {
                    ModelState.AddModelError(uSUARIOS.RUT, "Error");
                    return View(uSUARIOS);
                }

                db.USUARIOS.Add(uSUARIOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uSUARIOS);
        }

        // GET: USUARIOS/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null) {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RUT,NOMBRE_C,CORREO,UNE")] USUARIOS uSUARIOS) {
            if (ModelState.IsValid) {
                try {
                    
                    db.Entry(uSUARIOS).State = EntityState.Modified;
                    db.SaveChanges();
                    
                } catch (Exception) {
                    ModelState.AddModelError("","Error, Problema de coincidencias ( Usuario ya existe )");
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null) {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            db.USUARIOS.Remove(uSUARIOS);
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
        //public JsonResult DoesRutExist(string Rut, string Id, FormContext frm1, FormCollection frm2) {
        public JsonResult DoesRutExist(string Rut) {
            var user = db.USUARIOS.FirstOrDefault(p => p.RUT == Rut);

            return Json(user == null);
        }

        [HttpPost]
        public JsonResult DoesCorreoExist(string Correo) {
            var email = db.USUARIOS.FirstOrDefault(p => p.CORREO == Correo);

            return Json(email == null);
        }
    }
}
