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


namespace AuraInventarioProto.Controllers {
    //[Authorize]
    public class HomeController : Controller {
        private AuraInventarioProtoDBEntities1 db = new AuraInventarioProtoDBEntities1();

        // GET: Home
        [AllowAnonymous]
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LOGIN objLogin) {
            if (ModelState.IsValid) {
                var obj = db.LOGIN.Where(a => a.CORREO.Equals(objLogin.CORREO) && a.PASS.Equals(objLogin.PASS)).FirstOrDefault();
                if (obj != null) {
                    

                    Session["UserID"] = obj.ID.ToString();
                    Session["UserName"] = obj.CORREO.ToString();
                    return RedirectToAction("Index");
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
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