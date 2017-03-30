using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AuraInventarioProto.Models;

namespace AuraInventarioProto.ViewModels {

    public partial class Index_Usuarios {
        public int ID { get; set; }
        public string RUT { get; set; }
        public string NOMBRE_C { get; set; }
        public string CORREO { get; set; }
        public string UNE { get; set; }
        public string ESTADO { get; set; }
    }

    public partial class Index_Movimientos_Pc {
        public int ID { get; set; }
        public string RUT_USUARIO { get; set; }
        public string ID_PC { get; set; }
        public string TIPO_MOV { get; set; }
        public System.DateTime FECHA_MOV { get; set; }
        public string OBS { get; set; }
        public Index_Usuarios Usuarios { get; set; }
    }

    //public partial class Movimientos_PcIndexViewModel {
    //    AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities();
    //    public AuraInventarioProtoDBEntities Db { get => db; set => db = value; }
    //    public IEnumerable<Index_Movimientos_Pc> Movimientos_Pc = 
    //    public IEnumerable<Index_Usuarios> Usuarios { get; set; }
        
    //}

    //public partial class Movimientos_PcIndexViewModel {
    //    public IEnumerable<MOVIMIENTOS_PC> Movimientos_Pc { get; set; }
    //    public IEnumerable<USUARIOS> Usuarios { get; set; }


    //    //public Movimientos_PcIndexViewModel() {
    //    //    AuraInventarioProtoDBEntities db = new AuraInventarioProtoDBEntities;
    //    //    Movimientos_Pc = db.MOVIMIENTOS_PC.ToList();
    //    //    Usuarios = db.USUARIOS.ToList();

    //    //}
    //}
}