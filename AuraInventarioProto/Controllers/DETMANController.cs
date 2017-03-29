using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using AuraInventarioProto.ViewModels.ValidationViewModels;
using AuraInventarioProto.App_Start;
using System.Globalization;
using AutoMapper;

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
            INV_PC dETMAN = db.INV_PC.Find(id);
            if (dETMAN == null) {
                return HttpNotFound();
            }
            List<SelectListItem> Obras = new List<SelectListItem>();
            foreach (var obra in db.UNE) {
                Obras.Add(new SelectListItem { Text = obra.OBRA + " " + obra.DESCRIPCION, Value = obra.OBRA });
            }
            ViewBag.Obra = Obras;

            try {
                DateTime oldmantdate = db.DETMAN.Where(a => a.SERIAL == dETMAN.SERIAL).Max(a => a.F_UL_MAN);
                int oldmantid = db.DETMAN.Where(a => a.F_UL_MAN == oldmantdate).Max(a => a.ID);

                DETMAN oldmant = db.DETMAN.Find(oldmantid);

                ViewBag.serial = oldmant.SERIAL;
                ViewBag.modelo = oldmant.MODELO;
                ViewBag.marca = oldmant.MARCA;
                ViewBag.tipo = oldmant.TIPO;
                ViewBag.estado = oldmant.ESTADO;
                ViewBag.obs = oldmant.OBS;
                ViewBag.fecha_adq = oldmant.FECHA_ADQ.ToString("dd-MM-yyyy");
                ViewBag.est_tw = oldmant.EST_TW;
                ViewBag.est_cc = oldmant.EST_CC;
                ViewBag.est_av = oldmant.EST_AV;
                ViewBag.est_pd = oldmant.EST_PD;
                ViewBag.est_of = oldmant.EST_OF;
                ViewBag.est_wn = oldmant.EST_WN;
                ViewBag.est_reg = oldmant.EST_REG;
                ViewBag.sgi_sw = oldmant.SGI_SW;
                ViewBag.sgi_res = oldmant.SGI_RES;
                ViewBag.f_ul_man = oldmant.F_UL_MAN.ToString("dd-MM-yyyy");
                ViewBag.devu = oldmant.DEVU;
                ViewBag.asign = oldmant.ASIGN;
                ViewBag.obr = oldmant.OBRA;

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<DETMAN, DetManValidationViewModel>();
                });
                IMapper mapper = config.CreateMapper();
                var mant = mapper.Map<DETMAN, DetManValidationViewModel>(oldmant);
                mant.F_UL_MAN = DateTime.Today;
                return View(mant);
            } catch (Exception) {
                ViewBag.serial = "null";
                ViewBag.modelo = "null";
                ViewBag.marca = "null";
                ViewBag.tipo = "null";
                ViewBag.estado = "null";
                ViewBag.obs = "null";
                ViewBag.fecha_adq = "null"; ;
                ViewBag.est_tw = "null";
                ViewBag.est_cc = "null";
                ViewBag.est_av = "null";
                ViewBag.est_pd = "null";
                ViewBag.est_of = "null";
                ViewBag.est_wn = "null";
                ViewBag.est_reg = "null";
                ViewBag.sgi_sw = "null";
                ViewBag.sgi_res = "null";
                ViewBag.f_ul_man = "null";
                ViewBag.devu = "null";
                ViewBag.asign = "null";
                ViewBag.obr = "null";
                return View();
            }
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

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DETMAN, DetManValidationViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            var detman = mapper.Map<DETMAN, DetManValidationViewModel>(dETMAN);

            return View(detman);
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
