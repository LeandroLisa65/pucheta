using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Acciones
    {
            public int Id { set; get; }

            [Required]
            public string Detalle { set; get; }

            public int MenuId { set; get; }
            public string MenuDetalle { set; get; }

            [DisplayName("Permiso para Editar")]
            public bool Editar { set; get; }
            [DisplayName("Permiso para Borrar")]
            public bool Borrar { set; get; }

            [DisplayName("Ocultar Zona 1")]
            public bool OcultarZona1 { set; get; }
            [DisplayName("Ocultar Zona 2")]
            public bool OcultarZona2 { set; get; }
            [DisplayName("Ocultar Zona 3")]
            public bool OcultarZona3 { set; get; }
            [DisplayName("Ocultar Zona 4")]
            public bool OcultarZona4 { set; get; }


            public bool Activo { set; get; }
            public bool Borrado { set; get; }

    }
}
