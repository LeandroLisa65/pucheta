using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemPartidaPresupuestaria
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public float ImportePartida { get; set; }
        public float ImportePartidaAjustado { get; set; }
        public float ImporteFondoReparo { get; set; }
    }
}
