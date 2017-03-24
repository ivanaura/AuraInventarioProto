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
using LinqToExcel;
using System.Data.OleDb;
using System.Data.Entity.Validation;

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

            return View(uSUARIOS);
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
            //db.USUARIOS.Remove(uSUARIOS);
            uSUARIOS.ESTADO = "Inactivo";
            db.Entry(uSUARIOS).State = EntityState.Modified;
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
            string path = "/Doc/Usuarios.xlsx";
            return File(path, "application/vnd.ms-excel", "Usuarios.xlsx");
        }

        #region excel import
        [HttpPost]
        public JsonResult UploadExcel(USUARIOS users, HttpPostedFileBase FileUpload) {
            List<string> data = new List<string>();
            if (FileUpload != null) {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls")) {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    } else if (filename.EndsWith(".xlsx")) {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<USUARIOS>(sheetName) select a;

                    foreach (var a in artistAlbums) {
                        try {
                            if (a.NOMBRE_C != "" && a.RUT != "" && a.CORREO != "" && a.UNE != "" && a.ESTADO != "") {
                                USUARIOS TU = new USUARIOS();
                                TU.RUT = a.RUT;
                                TU.NOMBRE_C = a.NOMBRE_C;
                                TU.CORREO = a.CORREO;
                                TU.UNE = a.UNE;
                                TU.ESTADO = a.ESTADO;

                                db.USUARIOS.Add(TU);
                                db.SaveChanges();
                            } else {
                                data.Add("<ul>");
                                if (a.RUT == "" || a.RUT == null) data.Add("<li> Rut es requerido</li>");
                                if (a.NOMBRE_C == "" || a.NOMBRE_C == null) data.Add("<li> Nombre es requerido</li>");
                                if (a.CORREO == "" || a.CORREO == null) data.Add("<li>Correo es requerido</li>");
                                if (a.UNE == "" || a.UNE == null) data.Add("<li>Une es requerida</li>");
                                if (a.ESTADO == "" || a.ESTADO == null) data.Add("<li>Estado es requerido</li>");

                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        } catch (DbEntityValidationException ex) {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors) {
                                foreach (var validationError in entityValidationErrors.ValidationErrors) {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile))) {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                } else {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            } else {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}
