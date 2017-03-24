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
using System.Globalization;

namespace AuraInventarioProto.Controllers {
    [SessionExpire]
    public class DETMANController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: DETMen
        public ActionResult Index() {
            return View(db.DETMAN.ToList());
        }

        // GET: DETMen/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETMAN dETMAN = db.DETMAN.Find(id);
            if (dETMAN == null) {
                return HttpNotFound();
            }
            return View(dETMAN);
        }

        // GET: DETMen/Create
        public ActionResult Create(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETMAN dETMAN = db.DETMAN.Find(id);
            if (dETMAN == null) {
                return HttpNotFound();
            }
            List<SelectListItem> Obras = new List<SelectListItem>();
            foreach (var obra in db.UNE) {
                Obras.Add(new SelectListItem { Text = obra.OBRA + " " + obra.DESCRIPCION, Value = obra.OBRA });
            }
            ViewBag.Obra = Obras;

            //DateTime currentdate = DateTime.ParseExact(dETMAN.F_UL_MAN, "dd-MM-yyyy", CultureInfo.InvariantCulture);


            //DETMAN oldmant = (from a in db.DETMAN where a.SERIAL == dETMAN.SERIAL sle
                              
                              









            return View(dETMAN);
        }

        // POST: DETMen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN,OBRA")] DETMAN dETMAN) {
            if (ModelState.IsValid) {
                db.DETMAN.Add(dETMAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dETMAN);
        }

        // GET: DETMen/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETMAN dETMAN = db.DETMAN.Find(id);
            if (dETMAN == null) {
                return HttpNotFound();
            }
            return View(dETMAN);
        }

        // POST: DETMen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN,OBRA")] DETMAN dETMAN) {
            if (ModelState.IsValid) {
                db.Entry(dETMAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dETMAN);
        }

        // GET: DETMen/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETMAN dETMAN = db.DETMAN.Find(id);
            if (dETMAN == null) {
                return HttpNotFound();
            }
            return View(dETMAN);
        }

        // POST: DETMen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            DETMAN dETMAN = db.DETMAN.Find(id);
            db.DETMAN.Remove(dETMAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
