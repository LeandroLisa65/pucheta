using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class itemCertificadoDetalleActualizacionProyecto
    {
        public int IdNotaPedidoDetalle { get; set; }
        public double AvanceCargado { get; set; }
        public DateTime? FechaHastaCertif { get; set; }
        public int IdContratista { get; set; }
        public int Planificacion_Proyecto_ActividadesId { get; set; }
    }
}


//, int IdContratista, int Planificacion_Proyecto_ActividadesId