using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using AuraInventarioProto.ViewModels;
using System.Web.Security;
using static AuraInventarioProto.App_Start.HashClass;


namespace AuraInventarioProto.Controllers {
    //[Authorize]
    public class HomeController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: Home
        [AllowAnonymous]
        public ActionResult Index() {
            for (int i = 5; i >= 0; i--) {
                try {
                    var obj = db.USUARIOS.Where(a => a.RUT.Equals("00000000-0")).FirstOrDefault();
                    if (obj == null) {
                        USUARIOS uSUARIOS = new USUARIOS();
                        uSUARIOS.RUT = "00000000-0";
                        uSUARIOS.NOMBRE_C = "Informatica";
                        uSUARIOS.CORREO = "admin@aura.cl";
                        uSUARIOS.UNE = "OF";

                        db.USUARIOS.Add(uSUARIOS);
                        db.SaveChanges();
                        break;
                    }
                } catch (Exception) {
                    if (i == 0) {
                        //throw;
                    }                    
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel objLogin) {            
            if (ModelState.IsValid) {
                try {
                    string salt = (from n in db.LOGIN where objLogin.NOMBRE == n.NOMBRE select n.SALT).First().ToString();
                    string passtohash = objLogin.PASS + salt;
                    objLogin.PASS = CreateHash(passtohash);

                    var obj = db.LOGIN.Where(a => a.NOMBRE.Equals(objLogin.NOMBRE) && a.PASS.Equals(objLogin.PASS) && a.ESTADO.Equals("Activo")).FirstOrDefault();
                    if (obj != null) {
                        Session["UserID"] = obj.ID.ToString();
                        Session["UserName"] = obj.NOMBRE.ToString();
                        Session["UserRole"] = obj.ROL.ToString();
                        //return RedirectToAction("Index");
                        return View("Index");
                    }
                } catch (Exception) {
                    ViewBag.error = "Error, Datos invalidos.";
                    return View("Index");
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.error = "Error general.";
            //return RedirectToAction("Index");
            return View("Index");
        }

        //[HttpPost]
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index","Home");
            //return RedirectToRoute("/");
            //return View("Index");
        }

        //public ActionResult NotFound(string aspxerrorpath) {
        //    if (!string.IsNullOrWhiteSpace(aspxerrorpath))
        //        return RedirectToAction("NotFound");

        //    return View();
        //}
    }
}