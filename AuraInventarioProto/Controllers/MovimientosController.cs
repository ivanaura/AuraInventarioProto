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
    public class MovimientosController : Controller {
        // GET: Movimientos
        public ActionResult Index() {
            var movimientosmodel = new List<MovimientosModels>();
            SqlConnectionClass ins = new SqlConnectionClass();

            try {
                ins.Connect();
                using (ins.sqlcon) {
                    using (SqlCommand cmd = new SqlCommand("Select * from MOVIMIENTOS", ins.sqlcon)) {
                        ins.sqlcon.Open();
                        ins.sqldr = cmd.ExecuteReader();

                        while (ins.sqldr.Read()) {
                            var movimientos = new MovimientosModels();
                            movimientos.Id = Convert.ToInt32(ins.sqldr["ID"]);
                            movimientos.Id_Usuario = Convert.ToInt32(ins.sqldr["ID_USUARIO"]);
                            movimientos.Id_Pc = ins.sqldr["ID_PC"].ToString();
                            movimientos.Tipo_Mov = ins.sqldr["TIPO_MOV"].ToString();
                            movimientos.Fecha_As = ins.sqldr["FECHA_AS"].ToString();
                            movimientos.Fecha_Dv = ins.sqldr["FECHA_DV"].ToString();
                            movimientos.Fecha_Mov = ins.sqldr["FECHA_MOV"].ToString();
                            movimientosmodel.Add(movimientos);
                        }
                    }
                }
                ins.sqlcon.Close();
            } catch (SqlException ex) {
                ins.sqlcon.Close();
                Debug.WriteLine(ex.Message);
            }

            return View(movimientosmodel);
        }
    }
}