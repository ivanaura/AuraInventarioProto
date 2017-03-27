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
        [EmailAddress(ErrorMessage = "Favor entrar un Correo valido.")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        [Remote("DoesCorreoExist", "USUARIOS", HttpMethod = "POST", ErrorMessage = "Error, El Correo ya existe.")]
        public string CORREO { get; set; }

        [Display(Name = "Une:")]
        [Required(ErrorMessage = "Error, Une es requerido.")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string UNE { get; set; }

        [Display(Name = "Estado:")]
        public string ESTADO { get; set; }

    }
}