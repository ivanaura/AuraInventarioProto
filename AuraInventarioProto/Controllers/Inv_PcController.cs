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
    //[Authorize]
    [SessionExpire]
    public class INV_PCController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: INV_PC
        public ActionResult Index() {
            return View(db.INV_PC.ToList());
        }

        // GET: INV_PC/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_PC iNV_PC = db.INV_PC.Find(id);
            if (iNV_PC == null) {
                return HttpNotFound();
            }
            return View(iNV_PC);
        }

        // GET: INV_PC/Create
        public ActionResult Create() {
            return View();
        }

        // POST: INV_PC/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN_DEVU,OBRA")] INV_PC iNV_PC) {
            if (ModelState.IsValid) {
                db.INV_PC.Add(iNV_PC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iNV_PC);
        }

        // GET: INV_PC/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_PC iNV_PC = db.INV_PC.Find(id);
            if (iNV_PC == null) {
                return HttpNotFound();
            }
            return View(iNV_PC);
        }

        // POST: INV_PC/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN_DEVU,OBRA")] INV_PC iNV_PC) {
            if (ModelState.IsValid) {
                try {
                    db.Entry(iNV_PC).State = EntityState.Modified;
                    db.SaveChanges();
                } catch (Exception) {

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            return View(iNV_PC);
        }

        // GET: INV_PC/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_PC iNV_PC = db.INV_PC.Find(id);
            if (iNV_PC == null) {
                return HttpNotFound();
            }
            return View(iNV_PC);
        }

        // POST: INV_PC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            INV_PC iNV_PC = db.INV_PC.Find(id);
            db.INV_PC.Remove(iNV_PC);
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
        public JsonResult DoesSerialExist(string Serial) {
            var serial = db.INV_PC.FirstOrDefault(p => p.SERIAL == Serial);

            return Json(serial == null);
        }
    }
}
