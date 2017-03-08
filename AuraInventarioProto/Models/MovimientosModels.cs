using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuraInventarioProto.Models {
    public class MovimientosModels {
        public int Id { get; set; }
        public string Rut_Usuario { get; set; }
        public string Id_Pc { get; set; }
        public string Tipo_Mov { get; set; }
        public string Fecha_As { get; set; }
        public string Fecha_Dv { get; set; }
        public string Fecha_Mov { get; set; }
    }
}