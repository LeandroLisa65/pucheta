using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class ParteDiario
    {
        public int Id { set; get; }
        [DisplayName("Proyecto")]
        public int ProyectoId { set; get; }
        public Proyecto Proyecto { set; get; }

        [DisplayName("Propios")]
        public int Asig_Propios { set; get; }
        [DisplayName("Propios Presentes")]
        public int Asig_Propios_Presentes { set; get; }
        [DisplayName("Contratistas")]
        public int Asig_Contratista { set; get; }
        [DisplayName("Contratistas Presentes")]
        public int Asig_Contratista_Presentes { set; get; }
        [MaxLength(250)]        
        [DisplayName("Comentario")]
        public string Asig_Comentario { set; get; }
        [DisplayName("Covid Propios")]
        public int Covid_Propioa { set; get; }
        [DisplayName("Covid Contratistas")]
        public int Covid_Contratista { set; get; }
        [MaxLength(250)]
        [DisplayName("Comentario sobre Covid")]
        public string Covid_Comentario { set; get; }

        [DisplayName("Fecha de Creación")]
        public DateTime Fecha_Creacion { get; set; }

        public List<ParteDiario_Actividades> ParteDiario_Actividades { set; get; }
        public List<ParteDiario_Contratista> ParteDiario_Contratista { set; get; }
        public List<ParteDiario_Sol_ObrasAdic> ParteDiario_Sol_ObrasAdic { set; get; }
        public List<Contratistas> Contratistas { set; get;}
        public List<ParteDiario_Incidentes> ParteDiario_Incidentes { set; get;}
        public List<ParteDiario_Asistencia> ParteDiario_Asistencia { set; get;}
        
        public string FechaCreacion
        {
            get
            {
                return this.Fecha_Creacion.ToString("dd-MM-yyyy");
            }
        }

    }
}
