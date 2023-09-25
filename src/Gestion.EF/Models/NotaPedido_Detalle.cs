using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class NotaPedido_Detalle
    {
        public long Id { set; get; }
        public long IdNotaPedido { set; get; }
        public long IdPPA { set; get; }
        public long IdPA { set; get; }
        public long IdUbicacion { set; get; }

        public long IdIndiceAjuste { set; get; }
        public double Cantidad { set; get; }
        public double Precio_Contradado { set; get; }
        public double Avance_Actual { set; get; }
        public string UnidadMedida { set; get; }

        public bool Finalizado { set; get; }


        [NotMapped]
        public string PresentaPoliza { set; get; }
        [NotMapped]
        public string ComentarioPoliza { set; get; }
        [NotMapped]
        public string _Ori_Cantidad { set; get; }
        [NotMapped]
        public string _Ori_Precio_Contradado { set; get; }
        [NotMapped]
        public string sNotaPedido { set; get; }
        [NotMapped]
        public string sEstadoPedido { set; get; }
        [NotMapped]
        public string sComentarioPedido { set; get; }
        [NotMapped]
        public string sFechaNotaPedido { set; get; }
        [NotMapped]
        public string sUsuarioCreo { set; get; }
        [NotMapped]
        public string sActividad { set; get; }
        [NotMapped]
        public string sUbicacion { set; get; }
        [NotMapped]
        public string sContratista { set; get; }
        [NotMapped]
        public string sIndice { set; get; }

        [NotMapped]
        public string _Cantidad_Planificada { set; get; }
        [NotMapped]
        public string _Precio_Contradado
        {
            get
            {
                return "$ " + String.Format("{0:0.00}", Precio_Contradado).Replace(',', '.');
            }
        }
        [NotMapped]
        public string _Cantidad_y_Medida
        {
            get
            {
                return (String.Format("{0:0.00}", Cantidad)).Replace(',', '.') + " " + UnidadMedida;
            }
        }
    }
}
