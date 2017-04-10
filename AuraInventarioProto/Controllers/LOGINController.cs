﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using AuraInventarioProto.ViewModels.ValidationViewModels;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using static AuraInventarioProto.App_Start.HashClass;
using AuraInventarioProto.App_Start;
using AutoMapper;

namespace AuraInventarioProto.Controllers {
    [SessionExpire]
    public class LOGINController : Controller {
        private AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();

        // GET: Index
        public ActionResult Index() {
            return View(db.LOGIN.ToList());
        }

        // GET: LOGIN/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGIN lOGIN = db.LOGIN.Find(id);
            if (lOGIN == null) {
                return HttpNotFound();
            }
            return View(lOGIN);
        }

        // GET: LOGIN/Create
        public ActionResult Create() {
            return View();
        }

        // POST: LOGIN/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE,PASS,ROL")] LOGIN lOGIN) {
            var config1 = new MapperConfiguration(cfg => {
                cfg.CreateMap<LOGIN, LoginValidationViewModel>();
            });
            IMapper mapper1 = config1.CreateMapper();
            var login = mapper1.Map<LOGIN, LoginValidationViewModel>(lOGIN);

            if (TryValidateModel(login)) {
                

                lOGIN.NOMBRE = lOGIN.NOMBRE.ToUpper();
                string salt = CreateSalt();
                lOGIN.SALT = salt;
                string pass = lOGIN.PASS + salt;
                lOGIN.PASS = CreateHash(pass);
                lOGIN.ESTADO = "Activo";

                //lOGIN.PASS = CreateHash(pass, new SHA256CryptoServiceProvider());


                db.LOGIN.Add(lOGIN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login);
        }



        // GET: LOGIN/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGIN lOGIN = db.LOGIN.Find(id);
            if (lOGIN == null) {
                return HttpNotFound();
            }
            lOGIN.PASS = null;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<LOGIN, LoginValidationViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            var login = mapper.Map<LOGIN, LoginValidationViewModel>(lOGIN);

            return View(login);
        }

        // POST: LOGIN/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,PASS,ROL,ESTADO")] LOGIN lOGIN) {
            if (ModelState.IsValid) {
                try {
                    string salt = CreateSalt();
                    lOGIN.SALT = salt;
                    string pass = lOGIN.PASS + salt;
                    lOGIN.PASS = CreateHash(pass);

                    db.Entry(lOGIN).State = EntityState.Modified;
                    db.SaveChanges();
                } catch (Exception) {

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(lOGIN);
        }

        // GET: LOGIN/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGIN lOGIN = db.LOGIN.Find(id);
            if (lOGIN == null) {
                return HttpNotFound();
            }
            return View(lOGIN);
        }

        // POST: LOGIN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            LOGIN lOGIN = db.LOGIN.Find(id);
            //db.LOGIN.Remove(lOGIN);
            lOGIN.ESTADO = "Inactivo";
            db.Entry(lOGIN).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Recover(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGIN lOGIN = db.LOGIN.Find(id);
            if (lOGIN == null) {
                return HttpNotFound();
            }
            return View(lOGIN);
        }

        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverConfirmed(int id) {
            LOGIN lOGIN = db.LOGIN.Find(id);
            lOGIN.ESTADO = "Activo";
            db.Entry(lOGIN).State = EntityState.Modified;
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
        public JsonResult DoesNameExist(string Nombre) {
            var user = db.LOGIN.FirstOrDefault(p => p.NOMBRE == Nombre);

            return Json(user == null);
        }

    }
}
