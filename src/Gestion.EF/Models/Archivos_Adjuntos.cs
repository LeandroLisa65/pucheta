using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Archivos_Adjuntos
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string NombreOriginal { get; set; }
        [MaxLength(250)]
        public string Archivo { get; set; }
        [MaxLength(250)]
        public string URL { get; set; }
        [MaxLength(25)]
        public string Extencion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        [MaxLength(50)]
        public string sUsuario { get; set; }

    }
}
