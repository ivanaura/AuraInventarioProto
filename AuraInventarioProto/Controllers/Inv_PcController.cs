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
    //[Authorize]
    [SessionExpire]
    public class INV_PCController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: INV_PC
        
        public ActionResult Index() {
            return View(db.INV_PC.ToList());
        }

        //GET: Mantencion
        //public ActionResult Mantencion() {
        //    return View()
        //}

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
            List<SelectListItem> Obras = new List<SelectListItem>();
            foreach (var obra in db.UNE) {
                Obras.Add(new SelectListItem { Text = obra.OBRA + " " + obra.DESCRIPCION, Value = obra.OBRA });
            }
            ViewBag.Obra = Obras;

            INV_PC inv = new INV_PC();
            inv.EST_TW = true;
            inv.EST_WN = true;
            inv.EST_REG = true;
            inv.EST_PD = true;
            inv.EST_OF = true;
            inv.EST_CC = true;
            inv.EST_AV = true;
            inv.SGI_RES = true;
            inv.SGI_SW = true;
            inv.FECHA_ADQ = DateTime.Today;
            inv.F_UL_MAN = inv.FECHA_ADQ;
            //inv.FECHA_ADQ = DateTime.Today;
            //inv.F_UL_MAN = DateTime.Today;

            return View(inv);
        }

        // POST: INV_PC/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN,OBRA")] INV_PC iNV_PC) {
        //    if (ModelState.IsValid) {
        //        db.INV_PC.Add(iNV_PC);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(iNV_PC);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN,OBRA")] INV_PC iNV_PC) {
            if (ModelState.IsValid) {

                iNV_PC.SERIAL = iNV_PC.SERIAL.ToUpper();
                iNV_PC.MODELO = iNV_PC.MODELO.ToUpper();
                iNV_PC.MARCA = iNV_PC.MARCA.ToUpper();
                iNV_PC.DEVU = "No";
                iNV_PC.ASIGN = "Informatica";

                db.INV_PC.Add(iNV_PC);
                db.SaveChanges();
              
                MOVIMIENTOS_PC mOVIMIENTOS_PC = new MOVIMIENTOS_PC();
                mOVIMIENTOS_PC.RUT_USUARIO = "00000000-0";
                mOVIMIENTOS_PC.ID_PC = iNV_PC.SERIAL;
                mOVIMIENTOS_PC.TIPO_MOV = "Adquisicion";
                mOVIMIENTOS_PC.FECHA_MOV = iNV_PC.FECHA_ADQ;
                db.MOVIMIENTOS_PC.Add(mOVIMIENTOS_PC);
                db.SaveChanges();

                DETMAN dETMAN = new DETMAN();

                 


                return RedirectToAction("Index");
            }
            //return RedirectToAction("Index");
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
            List<SelectListItem> Obras = new List<SelectListItem>();
            foreach (var obra in db.UNE) {
                Obras.Add(new SelectListItem { Text = obra.OBRA + " " + obra.DESCRIPCION, Value = obra.OBRA });
            }
            ViewBag.Obra = Obras;
            return View(iNV_PC);
        }

        // POST: INV_PC/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN,OBRA")] INV_PC iNV_PC) {
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
            //db.INV_PC.Remove(iNV_PC);
            iNV_PC.ESTADO = "De Baja";
            db.Entry(iNV_PC).State = EntityState.Modified;
            db.SaveChanges();

            MOVIMIENTOS_PC mOVIMIENTOS_PC = new MOVIMIENTOS_PC();
            mOVIMIENTOS_PC.RUT_USUARIO = "00000000-0";
            mOVIMIENTOS_PC.ID_PC = iNV_PC.SERIAL;
            mOVIMIENTOS_PC.TIPO_MOV = "De Baja";
            mOVIMIENTOS_PC.FECHA_MOV = DateTime.Today;
            db.MOVIMIENTOS_PC.Add(mOVIMIENTOS_PC);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        public ActionResult Recover(int id) {
            INV_PC iNV_PC = db.INV_PC.Find(id);
            iNV_PC.ESTADO = "Activo";
            db.Entry(iNV_PC).State = EntityState.Modified;
            db.SaveChanges();

            MOVIMIENTOS_PC mOVIMIENTOS_PC = new MOVIMIENTOS_PC();
            mOVIMIENTOS_PC.RUT_USUARIO = "00000000-0";
            mOVIMIENTOS_PC.ID_PC = iNV_PC.SERIAL;
            mOVIMIENTOS_PC.TIPO_MOV = "Recuperacion";
            mOVIMIENTOS_PC.FECHA_MOV = DateTime.Today;
            db.MOVIMIENTOS_PC.Add(mOVIMIENTOS_PC);
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
