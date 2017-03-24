using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AuraInventarioProto.Models;


namespace AuraInventarioProto.ViewModels {
    public class LOGINViewModel {
        public int ID { get; set; }

        [Display(Name = "Usuario:")]
        [Required(ErrorMessage = "Error, Usuario es requerido.")]
        [MaxLength(30, ErrorMessage = "Error, El Usuario tiene un limite de 30 caracteres.")]
        [Remote("DoesNameExist", "LOGIN", HttpMethod = "POST", ErrorMessage = "Error, El Usuario ya existe.")]
        public string NOMBRE { get; set; }

        [Display(Name = "Clave:")]
        [Required(ErrorMessage = "Error, Clave es requerida.")]
        [MinLength(4, ErrorMessage = "Error, Campo necesita almenos 4 caracteres.")]
        public string PASS { get; set; }

        public string SALT { get; set; }

        [Display(Name = "Rol:")]
        [Required(ErrorMessage = "Error, Rol es requerido.")]
        public string ROL { get; set; }

        [Display(Name = "Estado:")]
        public string ESTADO { get; set; }

    }
}