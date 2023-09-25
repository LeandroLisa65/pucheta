using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
     public  class ParteDiario_Incidentes
    {

        public int Id { set; get; }

        [DisplayName("Parte Diario")]
        public int ParteDiarioId { set; get; }
        public ParteDiario ParteDiario { set; get; }

        [DisplayName("Incidente")]
        public int IncidenteId { set; get;}
        public Incidentes Incidente { set; get; }

        [DisplayName("No Conformidad")]
        public int NoConformidadId { set; get; }

        [DisplayName("ContratistaId")]
        public int ContratistaId { set; get; }
        public Contratistas Contratista { set; get; }

        [DisplayName("Comentario")]
        public string Comentario { set; get; }

        [DisplayName("Solicitado Por")]
        public string SolicitadoPor { set; get; }

        [DisplayName("Criticidad")]
        public string Criticidad { set; get; }



        [NotMapped]
        public string sIncidente { set; get; }
        [NotMapped]
        public string sContratista { set; get; }
        [NotMapped]
        public bool IsArchivosIncidentes { set; get; }

        #region Propiedades Derivadas

        public string FechaParteDiario
        {
            get
            {
                string fecha = "Sin Parte Diario asignado";
                if(this.ParteDiario != null)
                {
                    fecha = this.ParteDiario.FechaCreacion;
                }
                return fecha;
            }
        }
        public string NombreTipo
        {
            get
            {
                string tipo = "Sin Tipo asignado";
                if(this.Incidente != null)
                {
                    tipo = this.Incidente.Nombre;
                }
                return tipo;
            }
        }
        public string NombreProyecto
        {
            get
            {
                string nombre = "Sin Proyecto asignado";
                if (this.ParteDiario != null)
                {
                    if (this.ParteDiario.Proyecto != null)
                        nombre = this.ParteDiario.Proyecto.Nombre;
                }
                return nombre;
            }
        }

        public string NombreContratista
        {
            get
            {
                string nombre = "Sin Contratista asignado";
                if(this.Contratista != null)
                {
                    nombre = this.Contratista.Nombre;
                }
                return nombre;
            }
        }

        #endregion
    }
}
