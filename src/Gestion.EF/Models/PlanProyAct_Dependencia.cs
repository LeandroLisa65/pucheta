using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class PlanProyAct_Dependencia
    {
        public int Id { set; get; }
        public int PPAPadreId { set; get; }
        [ForeignKey("PPAPadreId")]
        public Planificacion_Proyecto_Actividades PPAPadre { get; set; }
        public int PPAHijaId { set; get; }
        [ForeignKey("PPAHijaId")]
        public Planificacion_Proyecto_Actividades PPAHija { get; set; }
        public DateTime FecCreacion { get; set; }
        public int IdUsuarioCreo { get; set; }

        public int PredecessorId
        {
            get
            {
                return this.PPAPadreId;
            }
        }
        public int SuccessorId
        {
            get
            {
                return this.PPAHijaId;
            }
        }
        public int Type
        {
            get
            {
                return 1;
            }
        }
    }
}
