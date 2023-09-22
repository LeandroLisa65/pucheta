using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class IncidentesHistorial_Detalle
    {
        public int Id { set; get; }

        public int IdIncidente_Historial { set; get; }

        [DisplayName("Fecha Asigancion")]
        public DateTime Asignacion_Fecha { get; set; }

        public int Asignacion_IdArea { set; get; }

        [DisplayName("Area Asignada")]
        public string Asignacion_Area { set; get; }

        [DisplayName("Estado")]
        public string Estado { set; get; }

        [DisplayName("Descripción")]
        public string Detalle { set; get; }

        public string Modificacion_Usuario { get; set; }

    }
}
