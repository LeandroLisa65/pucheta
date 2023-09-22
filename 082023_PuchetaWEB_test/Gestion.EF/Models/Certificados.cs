using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Certificados
    {
        public long Id { set; get; }
        public long IdProyecto { set; get; }
        public long IdContratista { set; get; }
        public DateTime FechaCreacion { set; get; }
        public string NroCertificado { set; get; }
        public long? Secuenciador { set; get; }
        public long IdUsuarioCreo { set; get; }
        public string Estado { set; get; }

        [NotMapped]
        public string sProyecto { set; get; }

        [NotMapped]
        public string sContratista { set; get; }
        [NotMapped]
        public string sUsuario { set; get; }


    }
}
