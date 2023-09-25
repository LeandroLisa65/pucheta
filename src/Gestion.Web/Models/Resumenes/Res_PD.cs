using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class Res_PD
    {

        // PARTE DIARIO
        public int Id { set; get; }

        public int ProyectoId { set; get; }
        public string ProyectoNombre { set; get; }

        public int Asig_Propios { set; get; }
        public int Asig_Propios_Presentes { set; get; }
        public int Asig_Contratista { set; get; }
        public int Asig_Contratista_Presentes { set; get; }
        public string Asig_Comentario { set; get; }
        public int Covid_Propioa { set; get; }
        public int Covid_Contratista { set; get; }
        public string Covid_Comentario { set; get; }
        public string Fecha_Creacion { get; set; }


    }
}
