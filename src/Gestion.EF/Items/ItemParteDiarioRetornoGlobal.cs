using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;
using System.Linq;

namespace Gestion.EF
{
    public class ItemParteDiarioRetornoGlobal
    {
        public ParteDiario ParteDiario { set; get; }
        public List<ItemParteDiarioRetorno> ItemParteDiarioRetorno { set; get; }
        public string strSolicitadoPor { set; get; }
        public int intSolicitadoPor { set; get; }
        public List<string> lstDistinctUbicaciones { set; get; }
        public List<string> lstDistinctActividades { set; get; }
        public List<string> lstEstados
        {
            get
            {
                return ValoresUtiles.Get_lEstadosAlternativos_PPA();
            }
        }
    }
}
