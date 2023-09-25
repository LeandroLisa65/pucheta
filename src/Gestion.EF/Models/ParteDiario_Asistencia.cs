using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
     public  class ParteDiario_Asistencia
    {

        public int Id { set; get;}
        public int ParteDiarioId { set; get; }
        public int ContratistasId { get; set; }
        public Contratistas Contratistas { set; get;}

        [DisplayName("Propios")]
        public int Asig_Propios { set; get; }

        [DisplayName("Propios Presentes")]
        public int Asig_Propios_Presentes { set; get; }

        [DisplayName("Contratistas")]
        public int Asig_Contratista { set; get; }

        [DisplayName("Contratistas Presentes")]
        public int Asig_Contratista_Presentes { set; get; }
        
        [DisplayName("Comentario")]
        public string Asig_Comentario { set; get; }

        [DisplayName("Covid Propios")]
        public int Covid_Propioa { set; get; }

        [DisplayName("Covid Contratistas")]
        public int Covid_Contratista { set; get; }

        
        [DisplayName("Comentario sobre Covid")]
        public string Covid_Comentario { set; get; }

        [NotMapped]
        public string SContratista { set; get;}
    }
}
