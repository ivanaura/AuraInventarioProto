using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AuraInventarioProto.ViewModels.ValidationViewModels {
    public partial class UneValidationViewModel {

        public int ID { get; set; }
        public string OBRA { get; set; }
        public string DESCRIPCION { get; set; }
        public string ESTADO { get; set; }

    }
}