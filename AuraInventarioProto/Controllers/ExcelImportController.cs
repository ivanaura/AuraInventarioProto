using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using AuraInventarioProto.App_Start;
using LinqToExcel;
using System.Data.SqlClient;

namespace AuraInventarioProto.Controllers {
    public class ExcelImportController : Controller {
        AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();
        // GET: ExcelImport
        public ActionResult Index() {
            return View();
        }

        ///// <summary>  
        ///// This function is used to download excel format.  
        ///// </summary>  
        ///// <param name="Path"></param>  
        ///// <returns>file</returns>  
        //public FileResult DownloadExcel() {
        //    string path = "/Doc/Users.xlsx";
        //    return File(path, "application/vnd.ms-excel", "Users.xlsx");
        //}


        





    }
}







