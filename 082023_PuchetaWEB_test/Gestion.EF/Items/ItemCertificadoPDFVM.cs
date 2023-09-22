using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemCertificadoPDFVM
        
    {
        public DateTime FechaCreacion { get; set; }
        public string _FechaCreacion { get; set; }
        public string _NotadePedido { get; set; }
        public string _NombreUsuario { get; set; }
        public string _FechaCertificado { get; set; }
        public string _ContratistaCertificado { get; set; }
        public string _ProyectoCertificado { get; set; }
        public List<Certificados_detalle_planificado>  ListaActividades { get; set; }
        public List<Certificados_detalle_adicional> ListaAdicionales { get; set; }
        public List<Certificados_detalle_liquidacion> Listaliquidaciones { get; set; }

        public List<itemCertificado_CertificadoLiquidacionPPresupuestaria> ListaPartidaPresupuestaria { get; set; }   
    }
}
