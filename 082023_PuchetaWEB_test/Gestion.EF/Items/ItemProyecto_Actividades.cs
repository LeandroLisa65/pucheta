using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemProyecto_Actividades
    {
        public ItemProyecto_Actividades()
        {
            this.lPPA_Padres = new List<Planificacion_Proyecto_Actividades>();
        }

        public int IdProyecto { get; set; }
        public Planificacion_Proyecto_Actividades pp { set; get;}
        public List<Planificacion_Proyecto_Actividades> lPPA_Padres { set; get; }

        public List<Proyecto_Ubicaciones> Proyecto_Ubicaciones { set; get;}
        public List<Usuarios> Usuarios { set; get;}
        public List<Contratistas> Contratistas { set; get;}
        public List<Indices> Indices { set; get; }

        public List<Planificacion_Actividades> Planificacion_Actividades { set; get; }
        public List<Planificacion_Actividades_Rubro> Planificacion_Actividades_Rubro { set; get;}
        public List<Planificacion_Actividades_Rubro> Planificacion_Actividades_Rubro_Antecedente { set; get; }

        [NotMapped]
       public DateTime FechaInicioProyecto { set; get; }
    }
}
