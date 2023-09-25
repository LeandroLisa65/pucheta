using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class itemParteDiario_FiltroActividades
    {
        public int IdParteDiario { set; get; }
        public string FiltroUbicacion { set; get; }=string.Empty;
        public int IdFiltroActividad { set; get; }
        public string FiltroAvanceActualHoy { set; get; } = string.Empty;
        public string FiltroAvanceActualAyer { set; get; } = string.Empty;
        public string FiltroEstado { set; get; } = string.Empty;
    }
}
