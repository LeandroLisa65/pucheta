using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Items.ProyCertificados
{
    public class ItemProyCert_Contratista
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public int ContratistaId { get; set; }
        public int NumeroCertificado { get; set; }
        public string Estado { get; set; }
    }
}
