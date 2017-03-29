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
using System.Text;
using LinqToExcel;

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

            Inv_PcValidationViewModel inv = new Inv_PcValidationViewModel();
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

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<INV_PC, DETMAN>();
                });
                IMapper mapper = config.CreateMapper();
                var dETMAN = mapper.Map<INV_PC, DETMAN>(iNV_PC);

                db.DETMAN.Add(dETMAN);
                db.SaveChanges();

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

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<INV_PC, Inv_PcValidationViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            var inv = mapper.Map<INV_PC, Inv_PcValidationViewModel>(iNV_PC);

            return View(inv);
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

        public FileResult DownloadExcel() {
            string path = "/Format/Inv_pc.xlsx";
            return File(path, "application/vnd.ms-excel", "Inv_pc.xlsx");
        }




        #region excel import
        [HttpPost]
        public JavaScriptResult UploadExcel(HttpPostedFileBase FileUpload) {
            StringBuilder data = new StringBuilder();
            StringBuilder errors = new StringBuilder();
            if (FileUpload != null) {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {

                    string pathToExcelFile = (Server.MapPath("~/Doc/")) + (FileUpload.FileName);
                    FileUpload.SaveAs(pathToExcelFile);

                    var excel = new ExcelQueryFactory(pathToExcelFile);
                    var sheet = excel.GetWorksheetNames();
                    var columns = excel.GetColumnNames(sheet.First());
                    //int x = 0;

                    //var tochange = from c in excel.Worksheet(sheet.First()) select c;
                    //string stob;

                    //
                    //foreach (var item in tochange) {
                    //    if (item[x].Value.ToString() == "ok" || item[x].Value.ToString() == "Ok") {
                    //        test.Concat( new { })
                    //    }
                    //}
                    //IQueryable<Inv_PcValidationViewModel> test;
                    //    foreach (var item in ) {
                    //        if (item.EST_TW) {
                    //            item.EST_TW = true;
                    //        }
                    //    }
                    

                    var contents = from c in excel.Worksheet<Inv_PcValidationViewModel>(sheet.First()) select c;

                    int counter = 0, row = 2;


                    errors.AppendLine();
                    try {
                        foreach (var a in contents) {
                            if (row > 500) {break; }
                            row++;
                            AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();
                            if (db.UNE.FirstOrDefault(p => p.OBRA == a.OBRA) == null) {
                                errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: Une no Existe.");
                                counter++;
                            } else if (a.DEVU != "Si" && a.DEVU != "No") {
                                errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: Estado devolucion Invalido.");
                                counter++;
                            } else if (a.TIPO != "AIO" && a.TIPO != "Notebook") {
                                errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: Tipo Invalido.");
                                counter++;
                            } else if (a.ESTADO != "Operativo" && a.ESTADO != "De Baja" && a.ESTADO != "Malo") {
                                errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: Estado Invalido.");
                                counter++;
                            } else if (db.INV_PC.FirstOrDefault(p => p.SERIAL == a.SERIAL) != null) {
                                errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: Serial Duplicada.");
                                counter++;
                            } else if (a.FECHA_ADQ.Year <= 2005 || a.F_UL_MAN.Year <= 2005) {
                                errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: Fecha Invalida.");
                                counter++;
                            } else {

                                try {
                                    

                                    Inv_PcValidationViewModel u = a;
                                    //u.FECHA_ADQ = DateTime.ParseExact(a.FECHA_ADQ.ToShortDateString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                    //u.F_UL_MAN = DateTime.ParseExact(a.F_UL_MAN.ToShortDateString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                    ModelState.Clear();
                                    
                                    

                                    var config = new MapperConfiguration(cfg => {
                                        cfg.CreateMap<Inv_PcValidationViewModel, INV_PC>();
                                    });
                                    IMapper mapper = config.CreateMapper();
                                    var inv = mapper.Map<Inv_PcValidationViewModel, INV_PC>(u);
                                    if (inv.FECHA_ADQ.Year <= 2005 || inv.F_UL_MAN.Year <= 2005) {
                                        errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: Fecha Invalida.");
                                        counter++;
                                    } else {
                                        ValidateModel(inv);
                                        db.INV_PC.Add(inv);
                                        db.SaveChanges();
                                    }                                        
                                } catch (Exception) {
                                    db.Dispose();
                                    //var error = ModelState.Values.Select(v => v.Errors.Select(b => b.ErrorMessage).ToString());
                                    //string er = error.First().ToString();
                                    var query = from state in ModelState.Values
                                                from error in state.Errors
                                                select error.ErrorMessage;
                                    var er = query.ToArray();
                                    errors.AppendLine("Serial: " + a.SERIAL + " con errores en fila: " + row + " Causa: " + er.FirstOrDefault() + ".");
                                    counter++;
                                }
                            }
                        }
                    } catch (Exception) {
                        db.Dispose();
                        if ((System.IO.File.Exists(pathToExcelFile))) {
                            System.IO.File.Delete(pathToExcelFile);
                        }
                        return JavaScript("Error de formato, No se pudieron cargar registros.");
                    }

                    
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile))) {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    try {
                        if (contents.First() == null) {
                            
                        }
                    } catch (Exception) {
                        return JavaScript("Archivo Invalido");
                    }

                    
                    if (counter <= 0) {
                        data.AppendLine("¡Completado sin errores!");
                    } else {
                        data.AppendLine(counter + " Filas con errores, por favor verificar datos." + Environment.NewLine + errors);
                    }
                    return JavaScript(data.ToString());

                } else {
                    return JavaScript("Solo archivos con formato Excel son permitidos.");
                }
            } else {
                return JavaScript("Por favor seleccione un archivo.");
            }
        }
        #endregion

    }
}
