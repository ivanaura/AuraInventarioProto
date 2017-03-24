using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AuraInventarioProto.Models;


namespace AuraInventarioProto.ViewModels {
    public class UNEViewModel {
        public int ID { get; set; }

        [Display(Name = "Obra:")]
        [Required(ErrorMessage = "Error, Obra es requerido.")]
        public string OBRA { get; set; }

        [Display(Name = "Descripcion:")]
        public string DESCRIPCION { get; set; }

        [Display(Name = "Estado:")]
        public string ESTADO { get; set; }

    }
}