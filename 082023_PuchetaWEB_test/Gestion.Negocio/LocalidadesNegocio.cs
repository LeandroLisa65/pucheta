using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class LocalidadesNegocio
    {
        public List<Localidades> Get_Localidades(int ProvinciaID)
        {
            List<Localidades> Lista = new List<Localidades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Localidades.OrderBy(o => o.Localidad).Where(l => l.ProvinciasId == ProvinciaID).ToList();
            }

            return Lista;
        }
    }
}
