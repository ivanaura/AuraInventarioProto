using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AuraInventarioProto.ViewModels.ValidationViewModels {
    public partial class Inv_PcValidationViewModel {

        public int ID { get; set; }

        [Display(Name = "Serial:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        [MinLength(3, ErrorMessage = "Error, Campo necesita al menos 3 caracteres.")]
        [Remote("DoesSerialExist", "INV_PC", HttpMethod = "POST", ErrorMessage = "Error, La Serial ya existe.")]
        public string SERIAL { get; set; }

        [Display(Name = "Modelo:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string MODELO { get; set; }

        [Display(Name = "Marca:")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string MARCA { get; set; }

        [Display(Name = "Tipo:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string TIPO { get; set; }

        [Display(Name = "Estado:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string ESTADO { get; set; }

        [Display(Name = "Observaciones:")]
        [MaxLength(255, ErrorMessage = "Error, Campo tiene un limite de 255 caracteres.")]
        public string OBS { get; set; }

        [Display(Name = "Fecha Adquisicion:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(8, ErrorMessage = "Error, Campo tiene un limite de 8 caracteres.")]
        public DateTime FECHA_ADQ { get; set; }

        [Display(Name = "Estado TeamViewer:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool EST_TW { get; set; }

        [Display(Name = "Estado Ccleaner:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool EST_CC { get; set; }

        [Display(Name = "Estado Anti Virus:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool EST_AV { get; set; }

        [Display(Name = "Estado Pdf:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool EST_PD { get; set; }

        [Display(Name = "Licencia Office:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool EST_OF { get; set; }

        [Display(Name = "Licencia Windows:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool EST_WN { get; set; }

        [Display(Name = "Estado registros:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool EST_REG { get; set; }

        [Display(Name = "SGI Software:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool SGI_SW { get; set; }

        [Display(Name = "SGI Restricciones:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        public bool SGI_RES { get; set; }

        [Display(Name = "Fecha Ultima Mantencion:")]
        [Required(ErrorMessage = "Error, Campo es requerido.")]
        [MaxLength(8, ErrorMessage = "Error, Campo tiene un limite de 8 caracteres.")]
        public DateTime F_UL_MAN { get; set; }

        public string DEVU { get; set; }

        public string ASIGN { get; set; }

        [Display(Name = "Obra:")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string OBRA { get; set; }


    }
}