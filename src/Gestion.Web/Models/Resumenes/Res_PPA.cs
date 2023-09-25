using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class Res_PPA
    {
        public int Id { set; get; }
        public int IdPadre { get; set; }
        public int Proyecto_UbicacionesId { set; get; }
        public string Proyecto_UbicacionesNombre { set; get; }
        public int Planificacion_ActividadesId { set; get; }
        public string Planificacion_ActividadesNombre { set; get; }
        public int Planificacion_Actividades_RubroId { set; get; }
        public string Planificacion_Actividades_RubroNombre { set; get; }
        public string Fecha_Creacion { get; set; }
        public string Fecha_Ultima_Modificacion { get; set; }
        public string Fecha_Real_Incio { get; set; }
        public string Fecha_Real_Fin { get; set; }
        public string Fecha_Est_Incio { get; set; }
        public string Fecha_Est_Fin { get; set; }
        public int UsuariosId { get; set; }
        public string UsuariosNombre { set; get; }
        public int ContratistasId { get; set; }
        public string ContratistasNombre { set; get; }
        public String Detalle { set; get; } //Comentarios que se agregaron a ala Actividada
        public float Incidencia { set; get; }
        public bool EsAdicional { get; set; }
        public float Cantidad { set; get; }
        public String UnidadMedida { set; get; }
        public float Avance { set; get; }
        public bool Finalizados { set; get; }

        public string TienePadre { get; set; }

        public string AsignadoNP { get; set; }
        public Double DisponibleCantidad { get; set; }
        public bool OcultarNP { get; set; }
    }
}
