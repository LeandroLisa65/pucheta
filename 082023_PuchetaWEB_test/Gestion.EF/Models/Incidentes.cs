using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
     public class Incidentes
    {
        public int Id { set; get;}

        [MaxLength(100)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre")]
        public string Nombre { set; get;}

        [DisplayName("Descripción")]
        public string Descripcion { set; get;}
        [DisplayName("Tipo Incidente")]
        public int TipoIncidenteId { set; get;}
      

        [MaxLength(10)]
        [DisplayName("Criticidad")]
        public string Criticidad { set; get;}

        [DisplayName("Genera Email")]
        public bool GeneraEmail { set; get;}

        [DisplayName("Lista de Email")]
        public string ListaEmail { set; get;}

        [DisplayName("Responsable")]
        public string RolResponsable { set; get;}


        [DisplayName("Genera Seg Incidente")]
        public bool GeneraSegIncidente { set; get; }

        [DisplayName("Tipo")]
        public string Rectype { set; get; }

    }
}
