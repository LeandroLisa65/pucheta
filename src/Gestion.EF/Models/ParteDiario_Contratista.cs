using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class ParteDiario_Contratista
    {
        public int Id { set; get; }
        public int ParteDiarioId { set; get; }
        public ParteDiario ParteDiario { set; get; }
        [DisplayName("Contratista")]
        public int UsuariosId { get; set; }
        public Usuarios Usuarios { get; set; }
        [MaxLength(250)]
        public string Comentario { get; set; }
    }
}
