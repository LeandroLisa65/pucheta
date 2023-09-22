using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemFinalizaActividad
    {

        public int Id { set; get; }
        public int IdPlanificacion_Proyecto_Actividad { set; get; }
        public int IdParteDiario { set; get; }
        public int IdCalidad_Item { set; get; }
        public string Valor { set; get; }
        public string Descripcion { set; get; }

        public List<IFormFile> ArchivosF { get; set; }



    }
}
