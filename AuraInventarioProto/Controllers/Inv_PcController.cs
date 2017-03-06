using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuraInventarioProto.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AuraInventarioProto.Controllers
{
    public class Inv_PcController : Controller
    {
        // GET: Inv_Pc
        public ActionResult Index()
        {
            var inv_pcmodel = new List<Inv_PcModels>();
            SqlConnectionClass ins = new SqlConnectionClass();

            try {
                ins.Connect();
                using (ins.sqlcon) {
                    using (SqlCommand cmd = new SqlCommand("Select * from INV_PC", ins.sqlcon)) {
                        ins.sqlcon.Open();
                        ins.sqldr = cmd.ExecuteReader();

                        while (ins.sqldr.Read()) {
                            var inv_pc = new Inv_PcModels();
                            inv_pc.Serial = ins.sqldr["SERIAL"].ToString();
                            inv_pc.Modelo = ins.sqldr["MODELO"].ToString();
                            inv_pc.Marca = ins.sqldr["MARCA"].ToString();
                            inv_pc.Tipo = ins.sqldr["TIPO"].ToString();
                            inv_pc.Estado = ins.sqldr["ESTADO"].ToString();
                            inv_pc.Obs = ins.sqldr["OBS"].ToString();
                            inv_pc.Fecha_Adq = ins.sqldr["FECHA_ADQ"].ToString();
                            inv_pc.Est_Tw = ins.sqldr["EST_TW"].ToString();
                            inv_pc.Est_Cc = ins.sqldr["EST_CC"].ToString();
                            inv_pc.Est_Av = ins.sqldr["EST_AV"].ToString();
                            inv_pc.Est_Pd = ins.sqldr["EST_PD"].ToString();
                            inv_pc.Est_Of = ins.sqldr["EST_OF"].ToString();
                            inv_pc.Est_Wn = ins.sqldr["EST_WN"].ToString();
                            inv_pc.Est_Reg = ins.sqldr["EST_REG"].ToString();
                            inv_pc.Sgi_Sw = ins.sqldr["SGI_SW"].ToString();
                            inv_pc.Sgi_Res = ins.sqldr["SGI_RES"].ToString();
                            inv_pc.F_Ul_Man = ins.sqldr["F_UL_MAN"].ToString();
                            inv_pc.Devu = ins.sqldr["DEVU"].ToString();
                            inv_pc.Asign_Devu = ins.sqldr["ASIGN_DEVU"].ToString();
                            inv_pc.Obra = ins.sqldr["OBRA"].ToString();
                            inv_pcmodel.Add(inv_pc);
                        }
                    }
                }
                ins.sqlcon.Close();
            } catch (SqlException ex) {
                ins.sqlcon.Close();
                Debug.WriteLine(ex.Message);
            }

            return View(inv_pcmodel);
        }

        [Route("Inv_Pc/Ingreso")]
        public ActionResult Ingreso() {
            return View();
        }
    }
}