using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;

namespace Gestion.EF
{
     public class ItemParteDiario_Contratistas
    {
        public ParteDiario_Actividades_Contratista pac { set; get;}
        public List<Contratistas> Contratistas { set; get;}
        public List<IFormFile> Archivos { get; set; }
    }

    public class ItemResumen_Actividades_Contratista  //REgistro de un avance cargado a un contratista
    {
        public int ContratistasId { get; set; }
        public string sContratistas { get; set; }
        public int ParteDiario_Actividades_ContratistaId { set; get; }

        //Datos de la Actividad
        public int Planificacion_Proyecto_ActividadesId { set; get; }
        public string EstadoAltPPA { set; get; }
        public string sUbicacion { get; set; }
        public string sRubro { get; set; }
        public string sActividad { get; set; }
        public string sComentario { get; set; }
        public string Cantidad { set; get; }
        public string Avance { set; get; }
        public string ColorFondo { set; get; }
        public float f_Avance { set; get; }
        public float CantidadAcumulada { set; get; }
        public string _CantidadAcumulada { set; get; }
        

        public bool IsArchivo { set; get; }
        public string CantidadAyer { set; get; }

        public string NotaPedido { set; get; }
        public string _CantidadAsignadaNP { set; get; }

        public string FechaEstInicio { set; get; }
        public string FechaEstFin { set; get; }
        public string FechaRealInicio { set; get; }
        public string TrabajoHoy { set; get; }
    }
}
