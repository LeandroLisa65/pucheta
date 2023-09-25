using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Indice_Valores
    {
        public long Id { set; get; }
        public long IdIndice { set; get; }
        public int Mes { set; get; }
        public int Ano { set; get; }
        public double Valor { set; get; }
    }
}
