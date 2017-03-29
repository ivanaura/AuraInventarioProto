using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AuraInventarioProto.ViewModels.ValidationViewModels {
    public partial class UsuariosValidationViewModel {

        [Key]
        public int ID { get; set; }

        [Display(Name = "Rut:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [RegularExpression(pattern: "[0-9]{2}[0-9]{3}[0-9]{3}-[k-k0-9]{1}", ErrorMessage = "Error, Favor entrar un Rut valido, ejemplo '12345678-k'.")]
        [MaxLength(12, ErrorMessage = "Error, Campo tiene un limite de 12 caracteres.")]
        [Remote("DoesRutExist", "USUARIOS", HttpMethod = "POST", ErrorMessage = "Error, El Usuario ya existe.")]
        public string RUT { get; set; }

        [Display(Name = "Nombre Completo:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(40, ErrorMessage = "Error, Campo tiene un limite de 40 caracteres.")]
        public string NOMBRE_C { get; set; }

        [Display(Name = "Correo:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        //[EmailAddress(ErrorMessage = "Favor entrar un Correo valido.")]        
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        [Remote("DoesCorreoExist", "USUARIOS", HttpMethod = "POST", ErrorMessage = "Error, El Correo ya existe.")]
        [RegularExpression(pattern: @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Favor entrar un Correo valido.")]        
        public string CORREO { get; set; }

        [Display(Name = "Une:")]
        [Required(ErrorMessage = "Error, Une es requerido.")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string UNE { get; set; }

        [Display(Name = "Estado:")]
        public string ESTADO { get; set; }

    }
}