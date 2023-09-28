using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class ParteDiario_Actividades_Fotos
    {
        public int Id { set; get; }

        [DisplayName("Parte Diario Actividad")]
        public int ParteDiario_ActividadesId { set; get; }
        public ParteDiario_Actividades ParteDiario_Actividades { set; get; }
        [MaxLength(250)]
        [DisplayName("Foto")]
        public string URL_Foto { set; get; }
    }
}
