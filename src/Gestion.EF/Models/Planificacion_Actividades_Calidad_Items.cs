using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Planificacion_Actividades_Calidad_Items
    {
        public int Id {set; get;}

        public int IdPLanificacionActividades {set; get;}

        [MaxLength(200)]
        [DisplayName("Documentación Obra")]
        public string Documentacion_Obra {set; get;}

        [MaxLength(50)]
        [DisplayName("Se Efectua")]
        public string Se_Efectua { set; get; }

        [DisplayName("Perioricidad")]
        public int Perioricidad { set; get; }

        [MaxLength(50)]
        [DisplayName("Tolerancia")]
        public string Tolerancia { set; get; }

        [MaxLength(50)]
        [DisplayName("Realiza Accion")]
        public string Realiza_Accion { set; get; }

        [MaxLength(50)]
        [DisplayName("Control")]
        public string Controla { set; get; }

        [MaxLength(50)]
        [DisplayName("Como Controlar")]
        public string Como_Controlar { set; get; }

        [MaxLength(50)]
        [DisplayName("Con Elemento")]
        public string Con_Elemento { set; get; }

        [MaxLength(50)]
        [DisplayName("Con Plano")]
        public string Con_Plano { set; get; }

        [MaxLength(50)]
        [DisplayName("Donde")]
        public string Donde { set; get; }

        [MaxLength(1024)]
        [DisplayName("Observaciones Tecnicas o Comentarios")]
        public string Observaciones { set; get; }
        public bool Inactivo { set; get; }
        public DateTime Fecha_Ult_Modificacion { set; get; }
        public int IdUsuarioMOdifico { set; get; }



        [NotMapped]
        public string sUsuarioMOdifico { set; get; }


    }
}
