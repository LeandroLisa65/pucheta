using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class Planificacion_Actividades_Calidad_ItemsItem
    {

        public int Id { set; get; }

        public int IdPLanificacionActividades { set; get; }

        public string Documentacion_Obra { set; get; }

 
        public string Se_Efectua { set; get; }
        public int Perioricidad { set; get; }
        public string Tolerancia { set; get; }


        public string Realiza_Accion { set; get; }


        public string Controla { set; get; }

        public string Como_Controlar { set; get; }

        public string Con_Elemento { set; get; }

        public string Con_Plano { set; get; }
        public string Donde { set; get; }

        public string Observaciones { set; get; }
        public bool Inactivo { set; get; }
        public int Fecha_Ult_Modificacion { set; get; }
        public int IdUsuarioMOdifico { set; get; }


    }
}
