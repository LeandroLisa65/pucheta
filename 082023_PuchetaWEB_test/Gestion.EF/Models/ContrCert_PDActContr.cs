    using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class ContrCert_PDActContr
    {
        public int Id { get; set; }
        public int CertContrId { get; set; }
        public ContrCertificado CertContr { get; set; }
        public int PDActContrId { get; set; }
        public float Cantidad { get; set; }


        public ParteDiario_Actividades_Contratista PDActContr { get; set; }
        public float CantRealCertificada { get; set; }
    }
}
