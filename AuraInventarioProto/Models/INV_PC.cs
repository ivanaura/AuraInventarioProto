//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuraInventarioProto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class INV_PC
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Error, Serial es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string SERIAL { get; set; }

        [Required(ErrorMessage = "Error, Modelo es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string MODELO { get; set; }

        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string MARCA { get; set; }

        [Required(ErrorMessage = "Error, Tipo es requerido")]
        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string TIPO { get; set; }

        [Required(ErrorMessage = "Error, Estado es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string ESTADO { get; set; }

        [MaxLength(255, ErrorMessage = "Error, Campo tiene un limite de 255 caracteres.")]
        public string OBS { get; set; }

        [Required(ErrorMessage = "Error, Fecha Adquisicion es requerido")]
        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string FECHA_ADQ { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string EST_TW { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string EST_CC { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string EST_AV { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string EST_PD { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string EST_OF { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string EST_WN { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string EST_REG { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string SGI_SW { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string SGI_RES { get; set; }

        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string F_UL_MAN { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string DEVU { get; set; }

        [MaxLength(30, ErrorMessage = "Error, Campo tiene un limite de 30 caracteres.")]
        public string ASIGN_DEVU { get; set; }

        [MaxLength(10, ErrorMessage = "Error, Campo tiene un limite de 10 caracteres.")]
        public string OBRA { get; set; }
    }
}