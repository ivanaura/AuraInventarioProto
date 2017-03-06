using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuraInventarioProto.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            return View();
        }

        //public ActionResult NotFound(string aspxerrorpath) {
        //    if (!string.IsNullOrWhiteSpace(aspxerrorpath))
        //        return RedirectToAction("NotFound");

        //    return View();
        //}
    }
}