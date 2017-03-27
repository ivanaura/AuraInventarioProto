using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AuraInventarioProto.ViewModels.ValidationViewModels {
    public partial class Movimientos_PcValidationViewModel {
        public int ID { get; set; }
        public string RUT_USUARIO { get; set; }
        public string ID_PC { get; set; }
        public string TIPO_MOV { get; set; }
        public System.DateTime FECHA_MOV { get; set; }
        public string OBS { get; set; }

    }
}