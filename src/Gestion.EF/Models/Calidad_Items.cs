using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Gestion.EF.Models
{
    public class Calidad_Items
    {
        public int Id { set; get; }
        [DisplayName("Destino")]
        public string Destino { set; get; }
        [DisplayName("Nombre")]
        public string Nombre { set; get; }
    }
}
