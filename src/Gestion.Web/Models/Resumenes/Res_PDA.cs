using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class Res_PDA
    {
        public int Id { set; get; }

        public int ParteDiarioId { set; get; }
        public string ParteDiarioNombre { set; get;}

        public int Planificacion_Proyecto_ActividadesId { set; get; }
        public string Planificacion_Proyecto_ActividadesNombre { set; get;}
        public int UsuariosId { set; get; }
        public String UsuariosNombre { set; get;}
        public float Avance { set; get; }
        public bool Finalizados { set; get; }
        public string Comentarios { set; get; }


    }
}
