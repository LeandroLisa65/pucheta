using Gestion.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF
{
    public class ItemAvences
    {

       public int id { set; get; } //: ubi[i].ubicacion.id,
        public string type { set; get; } //: "Real" o "Estimado",
        public string title { set; get; } //: ubi[i].ubicacion.nombre,
        public int IdProyUbicacion { get; set; }
        public string ubicacion { set; get; }
        public string rubro_nombre { set; get; } //: ubi[i].actividades[0].actividad.planificacion_Actividades_Rubro.nombre,
        public int IdPlanActividad { get; set; }
        public string actividades_nombre { set; get; } //: ubi[i].actividades[0].actividad.planificacion_Actividades.nombre,
        public string comentario { set; get; } //Comentario de la Actividad
        public int parentID { set; get; } //: 0,
        public int orderID { set; get; } //: 0,
        public string start { set; get; } //: new Date(ubi[i].actividades[0].actividad.sFecha_Real_Incio),
        public string fechaEstInicio { set; get; } 
        public string fechaEstInicioOriginal { set; get; }
        public string end { set; get; } //: new Date(ubi[i].actividades[0].actividad.sFecha_Real_Fin),
        public string fechaEstFin { set; get; } 
        public string fechaEstFinOriginal { set; get; } 
        public string fecInicioReal { get; set; }
        public string fecFinReal { get; set; }
        public float percentComplete { set; get; } //: 10,
        public float avance { set; get; } //: 10,
        public bool summary { set; get; } //: true,
        public bool expanded { set; get; } //: true
        public bool Finalizado { set; get; } //: true
        public bool Vencido { set; get; }
        public string Estado { set; get; }
        public string DetalleActPadres { get; set; }
        public string DetalleActHijas { get; set; }
    }
}

    