using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;

namespace Gestion.EF
{
     public class ItemActividades_Foto
    {
        public int Id { set; get; }
        
        public int ParteDiario_ActividadesId { set; get; }
        public List<ParteDiario_Actividades> ParteDiario_Actividades { set; get; }


        public string URL_Foto { set; get; }

        public IFormFile File01 { get; set; }
    }
}
