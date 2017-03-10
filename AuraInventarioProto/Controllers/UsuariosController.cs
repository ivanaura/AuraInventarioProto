using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AuraInventarioProto.Controllers {
    public class UsuariosController : Controller {
        // GET: Usuarios
        [Route("Usuarios/Detalles/{rut}/")]
        public ActionResult Detalles(string rut) {
            var usuariodetalle = new List<UsuariosModels>();
            SqlConnectionClass ins = new SqlConnectionClass();
            try {
                ins.Connect();
                using (ins.sqlcon) {
                    using (SqlCommand cmd = new SqlCommand("Select * from USUARIOS where rut='" + rut + "'", ins.sqlcon)) {
                        ins.sqlcon.Open();
                        ins.sqldr = cmd.ExecuteReader();

                        while (ins.sqldr.Read()) {
                            var usuario = new UsuariosModels();
                            usuario.Rut = ins.sqldr["RUT"].ToString();
                            usuario.Nombre = ins.sqldr["NOMBRE_C"].ToString();
                            usuario.Correo = ins.sqldr["CORREO"].ToString();
                            usuario.Une = ins.sqldr["UNE"].ToString();
                            usuariodetalle.Add(usuario);
                        }
                    }
                }
                ins.sqlcon.Close();
            } catch (SqlException ex) {
                ins.sqlcon.Close();
                Debug.WriteLine(ex.Message);
            }

            return View(usuariodetalle);

        }

        [Route("Usuarios/Ingreso/")]
        public ActionResult Ingreso() {
            return View();
        }

        public ActionResult Index() {

            var usuariosmodel = new List<UsuariosModels>();
            SqlConnectionClass ins = new SqlConnectionClass();

            try {
                ins.Connect();
                using (ins.sqlcon) {
                    using (SqlCommand cmd = new SqlCommand("Select * from USUARIOS", ins.sqlcon)) {
                        ins.sqlcon.Open();
                        ins.sqldr = cmd.ExecuteReader();

                        while (ins.sqldr.Read()) {
                            var usuario = new UsuariosModels();
                            usuario.Rut = ins.sqldr["RUT"].ToString();
                            usuario.Nombre = ins.sqldr["NOMBRE_C"].ToString();
                            usuario.Correo = ins.sqldr["CORREO"].ToString();
                            usuario.Une = ins.sqldr["UNE"].ToString();
                            usuariosmodel.Add(usuario);
                        }
                    }
                }
                ins.sqlcon.Close();
            } catch (SqlException ex) {
                ins.sqlcon.Close();
                Debug.WriteLine(ex.Message);
            }

            return View(usuariosmodel);

            #region
            /*
try {                
    string cmd = "Select * from USUARIOS";
    SqlCommand com = new SqlCommand(cmd, sql.sqlcon);
    SqlDataReader dr;
    sql.Connect();
    using (sql.sqlcon) {                    
        sql.sqlcon.Open();
        dr = com.ExecuteReader();
        while (dr.Read()) {
            var usuario = new UsuariosModels();
            usuario.Id = Convert.ToInt32(dr["ID"]);
            usuario.Nombre = dr["NOMBRE"].ToString();
            usuario.Correo = dr["CORREO"].ToString();
            usuario.Une = dr["UNE"].ToString();
            usuariosmodel.Add(usuario);
        }
    }
    sql.sqlcon.Close();                
} catch (SqlException ex) {
    sql.sqlcon.Close();
    Debug.WriteLine(ex.Message);
}
*/
            #endregion
        }
    }
}