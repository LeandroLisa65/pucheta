using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemProyCert_PDActContr
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public int ContratistaId { get; set; }
        public float Cantidad { get; set; }
        public float Monto { get; set; }
        public string Actividad { get; set; }
        public string ActividadDescripcion { get; set; }
        public string Ubicacion { get; set; }
        public string UnidadMedida { get; set; }
        public string CodigoPartidaPresupuestaria { get; set; }
        public float CantidadAcumAnterior { get; set; }
        public float ImporteAcumAnterior { get; set; }
        public bool MontosAjustables { get; set; }

    }
}
