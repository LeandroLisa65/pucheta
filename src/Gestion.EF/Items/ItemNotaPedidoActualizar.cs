using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemNotaPedidoActualizar
    {

        public long IdProyecto { set; get; }
        public long IdContratista { set; get; }
        public string Comentario { set; get; }
        public DateTime Fecha_Creacion { set; get; }
        public string NroNP { set; get; }
        public string Estado { set; get; } //Abierto - Ejecucion - Finalizado
        public long? Secuencial { set; get; }

        public long IdPPA { set; get; } //esto es planificacion
        public long IdPA { set; get; } //esto es actividad
        public long IdUbicacion { set; get; }

        public long IdIndiceAjuste { set; get; }
       
        public bool? PresentaPoliza { set; get; }

        public string ComentarioPoliza { set; get; }


        public double Cantidad { set; get; }
        public double Precio_Contradado { set; get; }

        [NotMapped]
        public string _Ori_Cantidad { set; get; }
        [NotMapped]
        public string _Ori_Precio_Contradado { set; get; }

    }
}
