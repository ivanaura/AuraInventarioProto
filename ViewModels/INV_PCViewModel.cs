using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AuraInventarioProto.Models;

namespace AuraInventarioProto.ViewModels {
    public class INV_PCViewModel {
        public int ID { get; set; }

        [Display(Name = "Serial:")]
        [Required(ErrorMessage = "Error, Serial es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        [Remote("DoesSerialExist", "INV_PC", HttpMethod = "POST", ErrorMessage = "Error, La Serial ya existe.")]
        public string SERIAL { get; set; }

        [Display(Name = "Modelo:")]
        [Required(ErrorMessage = "Error, Modelo es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string MODELO { get; set; }

        [Display(Name = "Marca:")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string MARCA { get; set; }

        [Display(Name = "Tipo:")]
        [Required(ErrorMessage = "Error, Tipo es requerido")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string TIPO { get; set; }

        [Display(Name = "Estado:")]
        [Required(ErrorMessage = "Error, Estado es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string ESTADO { get; set; }

        [Display(Name = "Observaciones:")]
        [MaxLength(255, ErrorMessage = "Error, Campo tiene un limite de 255 caracteres.")]
        public string OBS { get; set; }

        [Display(Name = "Fecha Adquisicion:")]
        [Required(ErrorMessage = "Error, Fecha Adquisicion es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string FECHA_ADQ { get; set; }

        [Display(Name = "Estado TeamViewer:")]
        public bool EST_TW { get; set; }

        [Display(Name = "Estado Ccleaner:")]
        public bool EST_CC { get; set; }

        [Display(Name = "Estado Anti Virus:")]
        public bool EST_AV { get; set; }

        [Display(Name = "Estado Pdf:")]
        public bool EST_PD { get; set; }

        [Display(Name = "Licencia Office:")]
        public bool EST_OF { get; set; }

        [Display(Name = "Licencia Windows:")]
        public bool EST_WN { get; set; }

        [Display(Name = "Estado registros:")]
        public bool EST_REG { get; set; }

        [Display(Name = "SGI Software:")]
        public bool SGI_SW { get; set; }

        [Display(Name = "SGI Restricciones:")]
        public bool SGI_RES { get; set; }

        [Display(Name = "Fecha Ultima Mantencion:")]
        [Required(ErrorMessage = "Error, Fecha De Ultima Mantencion es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string F_UL_MAN { get; set; }

        [Display(Name = "Devuelto:")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string DEVU { get; set; }

        [Display(Name = "Asignado Devolucion:")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string ASIGN { get; set; }

        [Display(Name = "Obra:")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string OBRA { get; set; }
    }
}