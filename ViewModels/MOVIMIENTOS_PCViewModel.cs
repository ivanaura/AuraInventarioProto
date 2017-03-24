using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AuraInventarioProto.Models;


namespace AuraInventarioProto.ViewModels {
    public class MOVIMIENTOS_PCViewModel {
        public int ID { get; set; }

        [Display(Name = "Rut Usuario:")]
        [Required(ErrorMessage = "Error, Rut es requerido.")]       
        [MaxLength(12, ErrorMessage = "Error, El Rut tiene un limite de 12 caracteres.")]
        [Remote("DoesRutExist", "MOVIMIENTOS_PC", HttpMethod = "POST", ErrorMessage = "Error, El Usuario no existe.")]
        public string RUT_USUARIO { get; set; }

        [Display(Name = "Serial Pc:")]
        [Required(ErrorMessage = "Error, Serial es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        [Remote("DoesPcExist", "MOVIMIENTOS_PC", HttpMethod = "POST", ErrorMessage = "Error, El Equipo no existe.")]
        public string ID_PC { get; set; }

        [Display(Name = "Tipo de Movimiento:")]
        [Required(ErrorMessage = "Error, Tipo es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string TIPO_MOV { get; set; }

        [Display(Name = "Fecha Movimiento:")]
        [Required(ErrorMessage = "Error, Fecha es requerido")]
        public string FECHA_MOV { get; set; }

        [Display(Name = "Observaciones:")]
        [MaxLength(255, ErrorMessage = "Error, Campo tiene un limite de 255 caracteres.")]
        public string OBS { get; set; }

    }
}