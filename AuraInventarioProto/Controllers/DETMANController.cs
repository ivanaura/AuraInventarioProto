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

        public ActionResult PDFpage(int? id) {
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
        [AccessValidator]
        public ActionResult Create(int? id, string serial) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            INV_PC dETMAN = db.INV_PC.Find(id);
            if (dETMAN == null) {
                return HttpNotFound();
            }

            int idd = db.INV_PC.FirstOrDefault(p => p.SERIAL == serial).ID;
            INV_PC invdet = db.INV_PC.Find(idd);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<INV_PC, DetManValidationViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            var invman = mapper.Map<INV_PC, DetManValidationViewModel>(invdet);


            List<SelectListItem> Obras = new List<SelectListItem>();
            foreach (var obra in db.UNE) {
                Obras.Add(new SelectListItem { Text = obra.OBRA + " " + obra.DESCRIPCION, Value = obra.OBRA });
            }
            ViewBag.Obrav = Obras;

            try {
                DateTime oldmantdate = db.DETMAN.Where(a => a.SERIAL == dETMAN.SERIAL).Max(a => a.F_UL_MAN);
                int idpc = db.DETMAN.Where(a => a.SERIAL == dETMAN.SERIAL).Max(a => a.ID);
                int oldmantid = db.DETMAN.Where(a => a.F_UL_MAN == oldmantdate && a.ID == idpc).Max(a => a.ID);
                



                DETMAN oldmant = db.DETMAN.Find(oldmantid);

                ViewBag.vserial = oldmant.SERIAL;
                ViewBag.vmodelo = oldmant.MODELO;
                ViewBag.vmarca = oldmant.MARCA;
                ViewBag.vtipo = oldmant.TIPO;
                ViewBag.vestado = oldmant.ESTADO;
                ViewBag.vobs = oldmant.OBS;
                ViewBag.vfecha_adq = oldmant.FECHA_ADQ.ToString("dd-MM-yyyy");
                ViewBag.vest_tw = oldmant.EST_TW;
                ViewBag.vest_cc = oldmant.EST_CC;
                ViewBag.vest_av = oldmant.EST_AV;
                ViewBag.vest_pd = oldmant.EST_PD;
                ViewBag.vest_of = oldmant.EST_OF;
                ViewBag.vest_wn = oldmant.EST_WN;
                ViewBag.vest_reg = oldmant.EST_REG;
                ViewBag.vsgi_sw = oldmant.SGI_SW;
                ViewBag.vsgi_res = oldmant.SGI_RES;
                ViewBag.vf_ul_man = oldmant.F_UL_MAN.ToString("dd-MM-yyyy");
                ViewBag.vdevu = oldmant.DEVU;
                ViewBag.vasign = oldmant.ASIGN;
                ViewBag.vobr = oldmant.OBRA;

                var config1 = new MapperConfiguration(cfg => {
                    cfg.CreateMap<DETMAN, DetManValidationViewModel>();
                });
                IMapper mapper1 = config1.CreateMapper();
                var mant = mapper1.Map<DETMAN, DetManValidationViewModel>(oldmant);
                mant.F_UL_MAN = DateTime.Today;
                mant.ASIGN = dETMAN.ASIGN;
                return View(mant);

            } catch (Exception) {
                ViewBag.vserial = "null";
                ViewBag.vmodelo = "null";
                ViewBag.vmarca = "null";
                ViewBag.vtipo = "null";
                ViewBag.vestado = "null";
                ViewBag.vobs = "null";
                ViewBag.vfecha_adq = "null"; ;
                ViewBag.vest_tw = "null";
                ViewBag.vest_cc = "null";
                ViewBag.vest_av = "null";
                ViewBag.vest_pd = "null";
                ViewBag.vest_of = "null";
                ViewBag.vest_wn = "null";
                ViewBag.vest_reg = "null";
                ViewBag.vsgi_sw = "null";
                ViewBag.vsgi_res = "null";
                ViewBag.vf_ul_man = "null";
                ViewBag.vdevu = "null";
                ViewBag.vasign = "null";
                ViewBag.vobr = "null";
                invman.F_UL_MAN = DateTime.Today;
                invman.ASIGN = dETMAN.ASIGN;
                return View(invman);
            }
        }

        // POST: DETMen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessValidator]
        public ActionResult Create([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN,OBRA")] DETMAN dETMAN) {
            var config1 = new MapperConfiguration(cfg => {
                cfg.CreateMap<DETMAN, DetManValidationViewModel>();
            });
            IMapper mapper1 = config1.CreateMapper();
            var der = mapper1.Map<DETMAN, DetManValidationViewModel>(dETMAN);

            if (ModelState.IsValid) {
                try {
                    db.DETMAN.Add(dETMAN);
                    db.SaveChanges();



                    //var cfg = new MapperConfiguration(cfge => { cfge.CreateMap<DETMAN, INV_PC>(); });
                    //IMapper map1 = cfg.CreateMapper();
                    //var pc = map1.Map<DETMAN, INV_PC>(dETMAN);

                    INV_PC pc = new INV_PC();
                    pc = db.INV_PC.Find(db.INV_PC.FirstOrDefault(p => p.SERIAL == dETMAN.SERIAL).ID);
                    pc.MARCA = dETMAN.MARCA;
                    pc.MODELO = dETMAN.MODELO;
                    pc.OBRA = dETMAN.OBRA;
                    pc.TIPO = dETMAN.TIPO;
                    pc.EST_AV = dETMAN.EST_AV;
                    pc.EST_CC = dETMAN.EST_CC;
                    pc.EST_OF = dETMAN.EST_OF;
                    pc.EST_PD = dETMAN.EST_PD;
                    pc.EST_REG = dETMAN.EST_REG;
                    pc.EST_TW = dETMAN.EST_TW;
                    pc.EST_WN = dETMAN.EST_WN;
                    pc.SGI_RES = dETMAN.SGI_RES;
                    pc.SGI_SW = dETMAN.SGI_SW;
                    pc.OBS = dETMAN.OBS;
                    pc.F_UL_MAN = dETMAN.F_UL_MAN;



                    db.Entry(pc).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("PDFpage", new { id = dETMAN.ID });
                } catch (Exception) {
                    return RedirectToAction("Create");
                }


                
            }

            return RedirectToAction("Create");
        }

        // GET: DETMen/Edit/5
        [AccessValidator]
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
        [AccessValidator]
        public ActionResult Edit([Bind(Include = "ID,SERIAL,MODELO,MARCA,TIPO,ESTADO,OBS,FECHA_ADQ,EST_TW,EST_CC,EST_AV,EST_PD,EST_OF,EST_WN,EST_REG,SGI_SW,SGI_RES,F_UL_MAN,DEVU,ASIGN,OBRA")] DETMAN dETMAN) {
            if (ModelState.IsValid) {




                db.Entry(dETMAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dETMAN);
        }

        // GET: DETMen/Delete/5
        [AccessValidator]
        [AdminValidator]
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
        [AccessValidator]
        [AdminValidator]
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
