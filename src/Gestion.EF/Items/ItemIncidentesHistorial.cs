using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;

namespace Gestion.EF
{
     public  class ItemIncidentesHistorial
    {

        public Incidentes_Historial ih { set; get; }

        public List<Proyecto> Proyecto { set; get; }
        public List<Contratistas> Contratistas { set; get;}
        public List<Incidentes> Incidentes { set; get; }
        public List<Usuarios> Usuarios { set; get;}
        public List<Roles> lstRoles { set; get; }
        public List<IncidentesHistorial_Detalle> lstHistorialDetalle { set; get; }
        public List<IFormFile> Archivos { get; set; }
        public string mUsuarioGraba { set; get; }
        public string mUsuarioActual { set; get; }
        public long mIdUsuarioActual { set; get; }

        public bool AgregarEmail { set; get;}
        public string IngresarEmail { set; get;}
        public List<Archivos_Adjuntos_Relacion> lArchivosAdjuntos { set; get; }

    }
}
