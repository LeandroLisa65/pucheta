using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemProyectoFile
    {
        public List<Proyecto> proyecto { set; get; }
        public List<IFormFile> Archivos { get; set; }

        public List<Proyecto_Ubicaciones> Proyecto_Ubicaciones { set; get; }

        public int ubicaciones { set; get; }

        public string Criticidad { set; get; }

        public string mUsuarioActual { set; get; }
        public long mIdUsuarioActual { set; get; }

        public string Asig_Comentario { set; get; }

        public string Comentario { set; get; }
        public string ComentarioHI { set; get; }
    }
}
