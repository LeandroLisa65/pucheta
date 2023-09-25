using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class FiltroPantallaGenerico
    {
        public string mfd { get; set; }// Fecha Desde
        public string mfh { get; set; }// Fecha Hasta
        public string strFiltroTexto1 { get; set; } //Un Filtro de texto
    }
}
