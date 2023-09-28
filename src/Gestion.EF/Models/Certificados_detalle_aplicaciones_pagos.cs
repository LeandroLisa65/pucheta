using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Certificados_detalle_aplicaciones_pagos
    {

        [Key]
        public int cap_Id { set; get; }
        public int cap_IdCertificados { set; get; }
        public string cap_NombreApellido { set; get; }
        public double? cap_Monto { set; get; }    
       
    }
}