using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItenCalidadParteDiario

    {
        public int IdCalidadItem { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set;}

        public List<itemCalidadObjeto> lIdsArchivosAdjuntos { get; set; }
    }


    public class itemCalidadObjeto
    {
        public int IdCalidad { get; set; }
        public int ImagenId { get; set; }
    }
}
