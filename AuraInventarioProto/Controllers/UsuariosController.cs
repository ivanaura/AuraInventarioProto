using AuraInventarioProto.App_Start;
using AuraInventarioProto.Models;
using AuraInventarioProto.ViewModels.ValidationViewModels;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

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
            List<SelectListItem> Obras = new List<SelectListItem>();
            foreach (var obra in db.UNE) {
                Obras.Add(new SelectListItem { Text = obra.OBRA + " " + obra.DESCRIPCION, Value = obra.OBRA });
            }
            ViewBag.Obra = Obras;

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

                uSUARIOS.NOMBRE_C = uSUARIOS.NOMBRE_C.ToUpper();
                uSUARIOS.UNE = uSUARIOS.UNE.ToUpper();
                uSUARIOS.RUT = uSUARIOS.RUT.ToUpper();
                uSUARIOS.ESTADO = "Activo";

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

            List<SelectListItem> Obras = new List<SelectListItem>();
            foreach (var obra in db.UNE) {
                Obras.Add(new SelectListItem { Text = obra.OBRA + " " + obra.DESCRIPCION, Value = obra.OBRA });
            }
            ViewBag.Obra = Obras;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<USUARIOS, UsuariosValidationViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            var usuarios = mapper.Map<USUARIOS, UsuariosValidationViewModel>(uSUARIOS);

            return View(usuarios);
        }

        // POST: USUARIOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RUT,NOMBRE_C,CORREO,UNE,ESTADO")] USUARIOS uSUARIOS) {
            if (ModelState.IsValid) {
                try {

                    db.Entry(uSUARIOS).State = EntityState.Modified;
                    db.SaveChanges();

                } catch (Exception) {
                    ModelState.AddModelError("", "Error, Problema de coincidencias ( Usuario ya existe )");
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
            //db.USUARIOS.Remove(uSUARIOS);
            uSUARIOS.ESTADO = "Inactivo";
            db.Entry(uSUARIOS).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Recover(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSURAIOS = db.USUARIOS.Find(id);
            if (uSURAIOS == null) {
                return HttpNotFound();
            }
            return View(uSURAIOS);
        }

        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverConfirmed(int id) {
            USUARIOS uSURAIOS = db.USUARIOS.Find(id);
            uSURAIOS.ESTADO = "Activo";
            db.Entry(uSURAIOS).State = EntityState.Modified;
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

        /// <summary>  
        /// This function is used to download excel format.  
        /// </summary>  
        /// <param name="Path"></param>  
        /// <returns>file</returns>  
        public FileResult DownloadExcel() {
            string path = "/Format/Usuarios.xlsx";
            return File(path, "application/vnd.ms-excel", "Usuarios.xlsx");
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

                    var contents = from c in excel.Worksheet<UsuariosValidationViewModel>(sheet.First()) select c;
                    int counter = 0, row = 2;
                    var rutpattern = @"[0 - 9]{ 2}[0-9]{3}[0-9]{3}-[k-k0-9]{1}";
                    var emailpattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                    var regexr = new System.Text.RegularExpressions.Regex(rutpattern);
                    var regexe = new System.Text.RegularExpressions.Regex(emailpattern);

                    errors.AppendLine();
                    foreach (var a in contents) {
                        if (row > 500) { break; }
                        row++;
                        AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();
                        if (db.UNE.FirstOrDefault(p => p.OBRA == a.UNE) == null) {
                            errors.AppendLine("Rut: " + a.RUT + " con errores en fila: " + row + " Causa: Une no Existe.");
                            counter++;
                        } else if (db.USUARIOS.FirstOrDefault(p => p.NOMBRE_C == a.NOMBRE_C) != null) {
                            errors.AppendLine("Rut: " + a.RUT + " con errores en fila: " + row + " Causa: Nombre Duplicado.");
                            counter++;
                        } else if (a.ESTADO != "Activo" && a.ESTADO != "Inactivo") {
                            errors.AppendLine("Rut: " + a.RUT + " con errores en fila: " + row + " Causa: Estado Invalido.");
                            counter++;
                        } else {
                            try {
                                
                                UsuariosValidationViewModel u = new UsuariosValidationViewModel() {
                                    RUT = a.RUT,
                                    NOMBRE_C = a.NOMBRE_C,
                                    CORREO = a.CORREO,
                                    UNE = a.UNE,
                                    ESTADO = a.ESTADO
                                };
                                ModelState.Clear();
                                ValidateModel(u);

                                var config = new MapperConfiguration(cfg => {
                                    cfg.CreateMap<UsuariosValidationViewModel, USUARIOS>();
                                });
                                IMapper mapper = config.CreateMapper();
                                var usuarios = mapper.Map<UsuariosValidationViewModel, USUARIOS>(u);

                                db.USUARIOS.Add(usuarios);
                                db.SaveChanges();

                            } catch (Exception) {
                                db.Dispose();
                                //var error = ModelState.Values.Select(v => v.Errors.Select(b => b.ErrorMessage).ToString());
                                //string er = error.First().ToString();
                                var query = from state in ModelState.Values
                                            from error in state.Errors
                                            select error.ErrorMessage;
                                var er = query.ToArray();
                                errors.AppendLine("Rut: " + a.RUT + " con errores en fila: " + row + " Causa: " + er.FirstOrDefault() + ".");
                                counter++;
                            }
                        }                        
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile))) {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    if (!contents.Any()) {
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

        #region bak
        //[HttpPost]
        //public JsonResult UploadExcel(USUARIOS users, HttpPostedFileBase FileUpload) {
        //    List<string> data = new List<string>();
        //    StringBuilder errors = new StringBuilder();
        //    if (FileUpload != null) {
        //        // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
        //        if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {

        //            string pathToExcelFile = (Server.MapPath("~/Doc/")) + (FileUpload.FileName);
        //            FileUpload.SaveAs(pathToExcelFile);

        //            var excel = new ExcelQueryFactory(pathToExcelFile);
        //            var sheet = excel.GetWorksheetNames();

        //            var contents = from c in excel.Worksheet<USUARIOS>(sheet.First()) select c;
        //            int counter = 0;
        //            int row = 2;

        //            errors.AppendLine();
        //            foreach (var a in contents) {
        //                if (row > 900) {
        //                    break;
        //                }
        //                try {
        //                    try {
        //                        if (a.NOMBRE_C != "" && a.RUT != "" && a.CORREO != "" && a.UNE != "" && a.ESTADO != "") {

        //                            USUARIOS u = new USUARIOS();
        //                            u.RUT = a.RUT;
        //                            u.NOMBRE_C = a.NOMBRE_C;
        //                            u.CORREO = a.CORREO;
        //                            u.UNE = a.UNE;
        //                            u.ESTADO = a.ESTADO;

        //                            db.USUARIOS.Add(u);
        //                            db.SaveChanges();
        //                            row++;
        //                        } else {
        //                            errors.AppendLine("Fila: " + row + " con errores.");
        //                            row++;
        //                            counter++;
        //                        }
        //                    } catch (Exception ex) {
        //                        //string err = ex.Message;                                
        //                        //errors.AppendLine("Rut: "+a.RUT+" con errores en fila: "+row+".");
        //                        //row++;
        //                        //counter++;
        //                    }

        //                } catch (DbEntityValidationException ex) {
        //                    foreach (var entityValidationErrors in ex.EntityValidationErrors) {
        //                        foreach (var validationError in entityValidationErrors.ValidationErrors) {
        //                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                        }
        //                    }
        //                }
        //            }
        //            //deleting excel file from folder  
        //            if ((System.IO.File.Exists(pathToExcelFile))) {
        //                System.IO.File.Delete(pathToExcelFile);
        //            }
        //            if (counter < 0) {
        //                data.Add("¡Completado sin errores!");
        //            } else {
        //                data.Add(counter + " Filas con errores, por favor verificar datos." + Environment.NewLine + errors);
        //            }
        //            ViewBag.errors = data;
        //            data.ToArray();
        //            return Json(data, JsonRequestBehavior.AllowGet);
        //        } else {
        //            //alert message for invalid file format  
        //            data.Add("Only Excel file format is allowed");
        //            return Json(data);
        //        }
        //    } else {
        //        if (FileUpload == null) data.Add("Please choose Excel file");
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}
        #endregion

        #region bak2
        //public JavaScriptResult UploadExcel(HttpPostedFileBase FileUpload) {
        //    StringBuilder data = new StringBuilder();
        //    StringBuilder errors = new StringBuilder();
        //    if (FileUpload != null) {
        //        if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {

        //            string pathToExcelFile = (Server.MapPath("~/Doc/")) + (FileUpload.FileName);
        //            FileUpload.SaveAs(pathToExcelFile);

        //            var excel = new ExcelQueryFactory(pathToExcelFile);
        //            var sheet = excel.GetWorksheetNames();

        //            var contents = from c in excel.Worksheet<UsuariosValidationViewModel>(sheet.First()) select c;
        //            int counter = 0, row = 2;
        //            var rutpattern = @"[0 - 9]{ 2}[0-9]{3}[0-9]{3}-[k-k0-9]{1}";
        //            var emailpattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        //            var regexr = new System.Text.RegularExpressions.Regex(rutpattern);
        //            var regexe = new System.Text.RegularExpressions.Regex(emailpattern);

        //            errors.AppendLine();
        //            foreach (var a in contents) {
        //                if (row > 500) {
        //                    break;
        //                }

        //                //if (db.UNE.FirstOrDefault(p => p.OBRA == a.UNE) == null ) {
        //                //    errors.AppendLine("Fila: " + row + " con errores (UNE no existe).");
        //                //    row++;
        //                //    counter++;
        //                //    break;
        //                //}
        //                ////if (db.USUARIOS.FirstOrDefault(p => p.NOMBRE_C == a.NOMBRE_C) != null) {
        //                ////    errors.AppendLine("Fila: " + row + " con errores (Nombre Duplicado).");
        //                ////    row++;
        //                ////    counter++;
        //                ////    break;
        //                ////}
        //                //if (db.USUARIOS.FirstOrDefault(p => p.RUT == a.RUT) != null) {
        //                //    errors.AppendLine("Fila: " + row + " con errores (Rut Duplicado).");
        //                //    row++;
        //                //    counter++;
        //                //    break;
        //                //}
        //                //if (db.USUARIOS.FirstOrDefault(p => p.CORREO == a.CORREO) != null) {
        //                //    errors.AppendLine("Fila: " + row + " con errores (Correo Duplicado).");
        //                //    row++;
        //                //    counter++;
        //                //    break;
        //                //}
        //                //if (a.ESTADO != "Activo" && a.ESTADO != "Inactivo") {
        //                //    errors.AppendLine("Fila: " + row + " con errores (Estado Invalido).");
        //                //    row++;
        //                //    counter++;
        //                //    break;
        //                //}


        //                try {
        //                    try {
        //                        //if (a.NOMBRE_C != null && a.RUT != null && a.CORREO != null && a.UNE != null && a.ESTADO != null &&
        //                        //    (!regexe.IsMatch(a.RUT)) && (!regexe.IsMatch(a.CORREO)) ) {
        //                        if (a.NOMBRE_C != null && a.RUT != null && a.CORREO != null && a.UNE != null && a.ESTADO != null) {
        //                            AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        //                            UsuariosValidationViewModel u = new UsuariosValidationViewModel() {
        //                                RUT = a.RUT,
        //                                NOMBRE_C = a.NOMBRE_C,
        //                                CORREO = a.CORREO,
        //                                UNE = a.UNE,
        //                                ESTADO = a.ESTADO
        //                            };
        //                            if (TryValidateModel(u)) {
        //                                var config = new MapperConfiguration(cfg => {
        //                                    cfg.CreateMap<UsuariosValidationViewModel, USUARIOS>();
        //                                });
        //                                IMapper mapper = config.CreateMapper();
        //                                var usuarios = mapper.Map<UsuariosValidationViewModel, USUARIOS>(u);

        //                                db.USUARIOS.Add(usuarios);
        //                                db.SaveChanges();
        //                                row++;
        //                            }

        //                        } else {
        //                            errors.AppendLine("Fila: " + row + " con errores.");
        //                            row++;
        //                            counter++;
        //                        }

        //                    } catch (Exception ex) {
        //                        db.Dispose();
        //                        ex.Data.Clear();
        //                        errors.AppendLine("Rut: " + a.RUT + " con errores en fila: " + row + ".");
        //                        row++;
        //                        counter++;

        //                    }
        //                } catch (DbEntityValidationException ex) {
        //                    foreach (var entityValidationErrors in ex.EntityValidationErrors) {
        //                        foreach (var validationError in entityValidationErrors.ValidationErrors) {
        //                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                        }
        //                    }
        //                }
        //            }

        //            //deleting excel file from folder  
        //            if ((System.IO.File.Exists(pathToExcelFile))) {
        //                System.IO.File.Delete(pathToExcelFile);
        //            }

        //            if (counter < 0) {
        //                data.AppendLine("¡Completado sin errores!");
        //            } else {
        //                data.AppendLine(counter + " Filas con errores, por favor verificar datos." + Environment.NewLine + errors);
        //            }

        //            return JavaScript(data.ToString());

        //        } else {
        //            return JavaScript("Solo archivos con formato Excel son permitidos.");
        //        }
        //    } else {
        //        return JavaScript("Por favor seleccione un archivo.");
        //    }
        //}
        #endregion

    }
}
