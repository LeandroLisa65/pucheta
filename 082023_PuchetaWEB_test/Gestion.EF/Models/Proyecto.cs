using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Proyecto
    {
        public int Id { set; get; }
        
        [MaxLength(250)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre del Proyecto")]
        public string Nombre { get; set; }

        [DisplayName("Fecha de Incio del Proyecto")]
        public DateTime Fecha_Inicio { get; set; }

        [DisplayName("Fecha de Fin del Proyecto")]
        public DateTime Fecha_Fin { get; set; }

        [DisplayName("Jefe de Obra")]
        public int UsuariosId { get; set; }
        [ForeignKey("UsuariosId")]
        public Usuarios Usuarios { get; set; }

        [DisplayName("Responsable S.H.")]
        public int RespSH_UsuarioId { get; set; }
        public Usuarios RespSH_Usuario { get; set; }

        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        public string Estado { get; set; }

        public List<Proyecto_Ubicaciones> Contratistas_Ubicaciones { get; set; }
        public List<ParteDiario> ParteDiario { get; set; }

        public int Duracion
        {
            get
            {
                return (int)(this.Fecha_Fin - this.Fecha_Inicio).TotalDays;
            }
        }
        public string FechaInicio
        {
            get
            {
                return this.Fecha_Inicio.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }
        public string FechaFin
        {
            get
            {
                return this.Fecha_Fin.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }

    }
}
