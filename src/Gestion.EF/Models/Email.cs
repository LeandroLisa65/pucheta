using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public DateTime FecCreacion { get; set; }
        public bool Enviado { get; set; }
        public int EntidadId { get; set; }
        public string Entidad { get; set; }
    }
}
