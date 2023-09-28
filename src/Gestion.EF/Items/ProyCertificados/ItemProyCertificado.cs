using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemProyCertificado
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int ProyectoId { get; set; }
        public string ProyectoNombre { get; set; }
        public int UsuarioCoordinadorProyecto { get; set; }
        public bool Cerrado { get; set; }
        public string FecDesde { get; set; }
        public string FecHasta { get; set; }
        public string FechaCreacion { get; set; }
        public int NumeroCertificado { get; set; }
        public List<ItemProyCert_PlanProyAct> lPCPPAs { get; set; }
    }
}
