//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuraInventarioProto.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;


    public partial class USUARIOS {
        public int ID { get; set; }

        [Display(Name = "Rut:")]
        [Required(ErrorMessage = "Error, Rut es requerido.")]
        [RegularExpression(pattern: "[0-9]{2}[0-9]{3}[0-9]{3}-[k-k0-9]{1}", ErrorMessage = "Error, Favor entrar un Rut valido, ejemplo '12345678-k'.")]
        [MaxLength(12, ErrorMessage = "Error, El Rut tiene un limite de 12 caracteres.")]
        [Remote("DoesRutExist", "USUARIOS", HttpMethod = "POST", ErrorMessage = "Error, El Usuario ya existe.")]
        public string RUT { get; set; }

        [Display(Name = "Nombre Completo:")]
        [Required(ErrorMessage = "Error, Nombre es requerido.")]
        [MaxLength(40, ErrorMessage = "Error, El Nombre tiene un limite de 40 caracteres.")]
        public string NOMBRE_C { get; set; }

        [Display(Name = "Correo:")]
        [Required(ErrorMessage = "Error, Correo es requerido.")]
        [EmailAddress(ErrorMessage = "Favor entrar un Correo valido.")]
        [MaxLength(30, ErrorMessage = "Error, El Correo tiene un limite de 30 caracteres.")]
        [Remote("DoesCorreoExist", "USUARIOS", HttpMethod = "POST", ErrorMessage = "Error, El Correo ya existe.")]
        public string CORREO { get; set; }

        [Display(Name = "Une:")]
        [Required(ErrorMessage = "Error, Une es requerido.")]
        [MaxLength(10, ErrorMessage = "Error, Une tiene un limite de 10 caracteres.")]
        public string UNE { get; set; }
    }
}