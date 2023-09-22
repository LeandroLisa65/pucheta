using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemNotaPedido
    {
        public long Id { get; set; }
        public long idProyecto { get; set; }
        public long? Secuencial { get; set; }

        public NotaPedido oNP { get; set; }

        public List<NotaPedido_Detalle>  lstNP_det { get; set; }
        public List<Proyecto> lstProyectos { get; set; }
        public List<Indices> lstIndices { get; set; }
        public List<Contratistas> lstContratistas { get; set; }
    }
}
