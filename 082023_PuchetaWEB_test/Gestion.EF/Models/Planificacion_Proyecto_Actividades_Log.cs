using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gestion.EF.Models
{
    public class Planificacion_Proyecto_Actividades_Log
    {
        public int Id { set; get; }
        [DisplayName("Contratistas Actividades")]
        public int Planificacion_Proyecto_ActividadesId { set; get; }
        public Planificacion_Proyecto_Actividades Planificacion_Contratistas_Actividades { set; get; }
        [DisplayName("Version")]
        public int Version { set; get; }
        [DisplayName("Fecha Estimada Inicio")]
        public DateTime Fecha_Est_Incio { get; set; }
        [DisplayName("Fecha Estimada Fin")]
        public DateTime Fecha_Est_Fin { get; set; }
    }
}
