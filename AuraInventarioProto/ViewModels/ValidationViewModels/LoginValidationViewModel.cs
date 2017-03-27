using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AuraInventarioProto.ViewModels.ValidationViewModels {
    public partial class LoginValidationViewModel {
        public int ID { get; set; }

        [Display(Name = "Usuario:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        [MinLength(3, ErrorMessage = "Error, Campo necesita al menos 3 caracteres.")]
        [Remote("DoesNameExist", "LOGIN", HttpMethod = "POST", ErrorMessage = "Error, El Usuario ya existe.")]
        public string NOMBRE { get; set; }

        [Display(Name = "Clave:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MinLength(4, ErrorMessage = "Error, Campo necesita al menos 4 caracteres.")]
        public string PASS { get; set; }
        public string SALT { get; set; }

        [Display(Name = "Rol:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public string ROL { get; set; }

        [Display(Name = "Estado:")]
        public string ESTADO { get; set; }

    }
}