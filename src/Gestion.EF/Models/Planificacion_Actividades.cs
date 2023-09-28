using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion.EF.Models
{
    public class Planificacion_Actividades
    {
        public int Id { set; get; }

        [DisplayName("Rubros")]
        public int Planificacion_Actividades_RubroId { set; get; }
        public Planificacion_Actividades_Rubro Planificacion_Actividades_Rubro { set; get; }

        [MaxLength(250)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre de la Actividad")]
        public string Nombre { get; set; }
        public int PartidaPresupuestariaId { get; set; }
        public PartidaPresupuestaria PartidaPresupuestaria { get; set; }

        [DisplayName("Descripción de la Actividad")]
        public string Descripción { get; set; }
        public bool Activo { get; set; }


        public List<Planificacion_Proyecto_Actividades> Planificacion_Proyecto_Actividades { set; get; }
        public List<Planificacion_Actividades_Tareas> Planificacion_Actividades_Tareas { set; get; }

        [NotMapped]
        public int Tiene_ItemsCalidad { get; set; } //0 NO TIENE - 1  SI TIENE

        #region PROPIEDADES DERIVADAS

        public string CodigoPartidaPresupuestaria
        {
            get
            {
                try
                {
                    string codigoPartidaPresupuestaria = "Sin Partida asignada";
                    if (this.PartidaPresupuestaria != null)
                        codigoPartidaPresupuestaria = this.PartidaPresupuestaria.Codigo;
                    return codigoPartidaPresupuestaria;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: PLanificacion_Actividades: CodigoPartidaPresupuestaria: exception.", ex);
                }
            }
        }

        public string DescripcionPartidaPresupuestaria
        {
            get
            {
                try
                {
                    string descripcionPartidaPresupuestaria = "Sin Partida asignada";
                    if (this.PartidaPresupuestaria != null)
                        descripcionPartidaPresupuestaria = this.PartidaPresupuestaria.Descripcion;
                    return descripcionPartidaPresupuestaria;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: PLanificacion_Actividades: DescripcionPartidaPresupuestaria: exception.", ex);
                }
            }
        }

        #endregion

    }
}
