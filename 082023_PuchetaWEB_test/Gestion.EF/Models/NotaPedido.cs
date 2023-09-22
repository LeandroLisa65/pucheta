using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class NotaPedido
    {
        public long Id { set; get; }
        public long IdProyecto { set; get; }
        
        public long IdContratista { set; get; }
        
        public string NroNP { set; get; }
        public string Comentario { set; get; }
        public string Estado { set; get; } //Abierto - Ejecucion - Finalizado
        public long IdUsuario_Creacion { set; get; }
        public DateTime Fecha_Creacion { set; get; }
        public DateTime Fecha_Cierre { set; get; }

        public long? Secuencial { set; get; }

        public long? IdIndiceAjuste { set; get; }

        
        public bool? PresentaPoliza { set; get; }

        public string ComentarioPoliza { set; get; }

        [NotMapped]
        public string sProyecto { set; get; }

        [NotMapped]
        public string sContratista { set; get; }
        [NotMapped]
        public string sUsuarioCreo { set; get; }
    }
}
