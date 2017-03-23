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
    public class MOVIMIENTOS_PCController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: MOVIMIENTOS_PC
        public ActionResult Index() {
            return View(db.MOVIMIENTOS_PC.ToList());
        }

        // GET: MOVIMIENTOS_PC/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            if (mOVIMIENTOS_PC == null) {
                return HttpNotFound();
            }
            return View(mOVIMIENTOS_PC);
        }

        // GET: MOVIMIENTOS_PC/Create
        public ActionResult Create(string selection) {
            List<SelectListItem> Usuarios = new List<SelectListItem>();
            List<SelectListItem> Equipos = new List<SelectListItem>();
            #region Default Block            

            foreach (var usuario in db.USUARIOS) {
                Usuarios.Add(new SelectListItem { Text = usuario.NOMBRE_C, Value = usuario.RUT });
            }
            foreach (var equipo in db.INV_PC) {
                Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
            }

            ViewBag.Rut = Usuarios;
            ViewBag.Pc = Equipos;
            #endregion

            MOVIMIENTOS_PC mov = new MOVIMIENTOS_PC();
            mov.FECHA_MOV = DateTime.Now.ToShortDateString().Replace("/","-");
            
            return View(mov);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult Create_logic(string selection) {
            List<SelectListItem> Usuarios = new List<SelectListItem>();
            List<SelectListItem> Equipos = new List<SelectListItem>();

            #region Logic Block

            if (selection == "Asignacion") {

                foreach (var usuario in db.USUARIOS) {
                    Usuarios.Add(new SelectListItem { Text = usuario.NOMBRE_C, Value = usuario.RUT });
                }
                foreach (var equipo in db.INV_PC) {
                    if (equipo.ESTADO == "Operativo" && equipo.DEVU == "No") {
                        Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
                    }
                }

                ViewBag.Rut = Usuarios;
                ViewBag.Pc = Equipos;
                return PartialView("_Create_Mov");

            } else if (selection == "Devolucion") {
                //TODO
                Usuarios.Add(new SelectListItem { Text = "Seleccione un equipo...", Value = "0" });



                foreach (var equipo in db.INV_PC) {
                    if (equipo.DEVU == "No") {
                        Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
                    }
                }

                ViewBag.Rut = Usuarios;
                ViewBag.Pc = Equipos;
                return PartialView("_Create_Mov");

            } else if (selection == "De Baja") {
                Usuarios.Add(new SelectListItem { Text = "Informatica", Value = "00000000-0" });

                foreach (var equipo in db.INV_PC) {
                    if (equipo.DEVU == "Si") {
                        Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
                    }
                }

                ViewBag.Rut = Usuarios;
                ViewBag.Pc = Equipos;
                return PartialView("_Create_Mov");
            }

            #endregion

            #region Default Block            

            foreach (var usuario in db.USUARIOS) {
                Usuarios.Add(new SelectListItem { Text = usuario.NOMBRE_C, Value = usuario.RUT });
            }
            foreach (var equipo in db.INV_PC) {
                Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
            }

            ViewBag.Rut = Usuarios;
            ViewBag.Pc = Equipos;

            #endregion

            return PartialView("_Create_Mov");
        }


        // POST: MOVIMIENTOS_PC/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RUT_USUARIO,ID_PC,TIPO_MOV,FECHA_AS,FECHA_DV,FECHA_MOV")] MOVIMIENTOS_PC mOVIMIENTOS_PC) {
            if (ModelState.IsValid) {

                if (mOVIMIENTOS_PC.TIPO_MOV == "Devolucion") {
                    int idpc = db.INV_PC.FirstOrDefault(p => p.SERIAL == mOVIMIENTOS_PC.ID_PC).ID;
                    INV_PC iNV_PC =  db.INV_PC.Find(idpc);

                    string user = db.USUARIOS.FirstOrDefault(u => u.NOMBRE_C == iNV_PC.ASIGN_DEVU).RUT;


                    mOVIMIENTOS_PC.RUT_USUARIO = user;



                    iNV_PC.DEVU = "Si";
                    iNV_PC.ASIGN_DEVU = "Informatica";
                    iNV_PC.OBRA = "OF";

                    db.MOVIMIENTOS_PC.Add(mOVIMIENTOS_PC);
                    db.SaveChanges();

                    db.Entry(iNV_PC).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                } else if (mOVIMIENTOS_PC.TIPO_MOV == "Asignacion") {
                    int idpc = db.INV_PC.FirstOrDefault(p => p.SERIAL == mOVIMIENTOS_PC.ID_PC).ID;
                    INV_PC iNV_PC = db.INV_PC.Find(idpc);

                    iNV_PC.DEVU = "No";
                    iNV_PC.ASIGN_DEVU = db.USUARIOS.FirstOrDefault(p => p.RUT == mOVIMIENTOS_PC.RUT_USUARIO).NOMBRE_C;

                    db.MOVIMIENTOS_PC.Add(mOVIMIENTOS_PC);
                    db.SaveChanges();

                    db.Entry(iNV_PC).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                db.MOVIMIENTOS_PC.Add(mOVIMIENTOS_PC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        // GET: MOVIMIENTOS_PC/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            if (mOVIMIENTOS_PC == null) {
                return HttpNotFound();
            }

            List<SelectListItem> Usuarios = new List<SelectListItem>();
            List<SelectListItem> Equipos = new List<SelectListItem>();
            foreach (var usuario in db.USUARIOS) {
                Usuarios.Add(new SelectListItem { Text = usuario.NOMBRE_C, Value = usuario.RUT });
            }
            foreach (var equipo in db.INV_PC) {
                Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
            }

            ViewBag.Rut = Usuarios;
            ViewBag.Pc = Equipos;

            return View(mOVIMIENTOS_PC);
        }

        // POST: MOVIMIENTOS_PC/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RUT_USUARIO,ID_PC,TIPO_MOV,FECHA_AS,FECHA_DV,FECHA_MOV")] MOVIMIENTOS_PC mOVIMIENTOS_PC) {
            if (ModelState.IsValid) {
                try {
                    db.Entry(mOVIMIENTOS_PC).State = EntityState.Modified;
                    db.SaveChanges();
                } catch (Exception) {

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(mOVIMIENTOS_PC);
        }

        // GET: MOVIMIENTOS_PC/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            if (mOVIMIENTOS_PC == null) {
                return HttpNotFound();
            }
            return View(mOVIMIENTOS_PC);
        }

        // POST: MOVIMIENTOS_PC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            MOVIMIENTOS_PC mOVIMIENTOS_PC = db.MOVIMIENTOS_PC.Find(id);
            db.MOVIMIENTOS_PC.Remove(mOVIMIENTOS_PC);
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
        public JsonResult DoesRutExist(string Rut_Usuario) {           
            var user = db.USUARIOS.FirstOrDefault(p => p.RUT == Rut_Usuario);

            return Json(user != null);
        }

        [HttpPost]
        public JsonResult DoesPcExist(string Id_Pc) {
            var pc = db.INV_PC.FirstOrDefault(p => p.SERIAL == Id_Pc);

            return Json(pc != null);
        }

        [HttpPost]
        public ActionResult Test(string selection) {
            
            if (selection == "Asignacion") {
                List<SelectListItem> Equipos = new List<SelectListItem>();                
                foreach (var equipo in db.INV_PC) {
                    if (equipo.ESTADO == "Operativo" && equipo.DEVU == "No") {
                        Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
                    }                    
                }
                ViewBag.Pc = Equipos;
            } else if (selection == "Devolucion") {
                List<SelectListItem> Equipos = new List<SelectListItem>();
                foreach (var equipo in db.INV_PC) {
                    if (equipo.DEVU == "No") {
                        Equipos.Add(new SelectListItem { Text = equipo.SERIAL, Value = equipo.SERIAL });
                    }
                }
                ViewBag.Pc = Equipos;

            }

            return Json(ViewBag);
        }
    }
}
