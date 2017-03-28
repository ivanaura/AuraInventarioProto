﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AuraInventarioProto.Models;

namespace AuraInventarioProto.ViewModels {
    public class DETMANViewModel {
        public int ID { get; set; }

        public string SERIAL { get; set; }
        public string MODELO { get; set; }
        public string MARCA { get; set; }
        public string TIPO { get; set; }
        public string ESTADO { get; set; }
        public string OBS { get; set; }
        public string FECHA_ADQ { get; set; }
        public bool? EST_TW { get; set; }
        public bool? EST_CC { get; set; }
        public bool? EST_AV { get; set; }
        public bool? EST_PD { get; set; }
        public bool? EST_OF { get; set; }
        public bool? EST_WN { get; set; }
        public bool? EST_REG { get; set; }
        public bool? SGI_SW { get; set; }
        public bool? SGI_RES { get; set; }
        public string F_UL_MAN { get; set; }
        public string DEVU { get; set; }
        public string ASIGN { get; set; }
        public string OBRA { get; set; }
    }
}