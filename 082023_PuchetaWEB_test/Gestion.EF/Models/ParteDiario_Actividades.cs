using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gestion.EF.Models
{
    public class ParteDiario_Actividades
    {
        public int Id { set; get; }
        [DisplayName("Parte Diario")]
        public int ParteDiarioId { set; get; }
        public ParteDiario ParteDiario { set; get; }
        [DisplayName("Proyecto Actividad")]
        public int Planificacion_Proyecto_ActividadesId { set; get; }
        public int Planificacion_ActividadesId { set; get; }
        public Planificacion_Proyecto_Actividades Planificacion_Proyecto_Actividades { set; get; }
        [DisplayName("Usuario a Cargo")]
        public int UsuariosId { set; get; }
        public Usuarios Usuarios { set; get; }
        /// <summary>
        /// Avance - Se calcula por medio de la relacion Cantidad en Plan_Proyecto_Actividad
        /// </summary>
        public float Avance { set; get; }
       
        /// <summary>
        /// Finalizados - True al llegar al Avance 100%
        /// </summary>
        public bool Finalizados { set; get; }


        public string Comentarios { set; get; }

        public List<ParteDiario_Actividades_Contratista> ParteDiario_Actividades_Contratista { set; get; }
        public List<ParteDiario_Actividades_Fotos> ParteDiario_Actividades_Fotos { set; get; }


    }
}
