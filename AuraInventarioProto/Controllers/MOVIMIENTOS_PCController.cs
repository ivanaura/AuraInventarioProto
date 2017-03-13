using System;
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
    public class MOVIMIENTOS_PCController : Controller
    {
        private AuraInventarioProtoDBEntities1 db = new AuraInventarioProtoDBEntities1();

        // GET: MOVIMIENTOS_PC
        public ActionResult Index()
        {
            return View(db.MOVIMIENTOS_PC.ToList());
        }

        // GET: MOVIMIENTOS_PC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            if (mOVIMIENTOS_PC == null)
            {
                return HttpNotFound();
            }
            return View(mOVIMIENTOS_PC);
        }

        // GET: MOVIMIENTOS_PC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MOVIMIENTOS_PC/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RUT_USUARIO,ID_PC,TIPO_MOV,FECHA_AS,FECHA_DV,FECHA_MOV")] MOVIMIENTOS_PC mOVIMIENTOS_PC)
        {
            if (ModelState.IsValid)
            {
                db.MOVIMIENTOS_PC.Add(mOVIMIENTOS_PC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mOVIMIENTOS_PC);
        }

        // GET: MOVIMIENTOS_PC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            if (mOVIMIENTOS_PC == null)
            {
                return HttpNotFound();
            }
            return View(mOVIMIENTOS_PC);
        }

        // POST: MOVIMIENTOS_PC/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RUT_USUARIO,ID_PC,TIPO_MOV,FECHA_AS,FECHA_DV,FECHA_MOV")] MOVIMIENTOS_PC mOVIMIENTOS_PC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mOVIMIENTOS_PC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mOVIMIENTOS_PC);
        }

        // GET: MOVIMIENTOS_PC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            if (mOVIMIENTOS_PC == null)
            {
                return HttpNotFound();
            }
            return View(mOVIMIENTOS_PC);
        }

        // POST: MOVIMIENTOS_PC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            db.MOVIMIENTOS_PC.Remove(mOVIMIENTOS_PC);
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
