using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using System.Web.Security;
using static AuraInventarioProto.App_Start.HashClass;


namespace AuraInventarioProto.Controllers {
    //[Authorize]
    public class HomeController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: Home
        [AllowAnonymous]
        public ActionResult Index() {
            var obj = db.USUARIOS.Where(a => a.RUT.Equals("00000000-0")).FirstOrDefault();
            if (obj == null) {
                USUARIOS uSUARIOS = new USUARIOS();
                uSUARIOS.RUT = "00000000-0";
                uSUARIOS.NOMBRE_C = "Informatica";
                uSUARIOS.CORREO = "admin@aura.cl";
                uSUARIOS.UNE = "OF";

                db.USUARIOS.Add(uSUARIOS);
                db.SaveChanges();
            }

                return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LOGIN objLogin) {
            if (ModelState.IsValid) {
                string salt = (from n in db.LOGIN where objLogin.NOMBRE == n.NOMBRE select n.SALT).First().ToString();
                string passtohash = objLogin.PASS + salt;
                objLogin.PASS = CreateHash(passtohash);

                var obj = db.LOGIN.Where(a => a.NOMBRE.Equals(objLogin.NOMBRE) && a.PASS.Equals(objLogin.PASS)).FirstOrDefault();
                if (obj != null) {                    
                    Session["UserID"] = obj.ID.ToString();
                    Session["UserName"] = obj.NOMBRE.ToString();
                    Session["UserRole"] = obj.ROL.ToString();
                    return RedirectToAction("Index");
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return RedirectToAction("Index");
        }

        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login2(LOGIN objLogin) {
            if (ModelState.IsValid) {
 
            }
            return RedirectToAction("Index");
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