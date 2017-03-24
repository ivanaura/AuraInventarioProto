namespace AuraInventarioProto.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class LoginViewModel {        
        [Display(Name = "Usuario:")]
        [Required(ErrorMessage = "Error, Usuario es requerido.")]
        [MaxLength(30, ErrorMessage = "Error, El Usuario tiene un limite de 30 caracteres.")]
        [Remote("DoesNameExist", "LOGIN", HttpMethod = "POST", ErrorMessage = "Error, El Usuario ya existe.")]
        public string NOMBRE { get; set; }

        [Display(Name = "Clave:")]
        [Required(ErrorMessage = "Error, Clave es requerida.")]
        public string PASS { get; set; }
    }
}