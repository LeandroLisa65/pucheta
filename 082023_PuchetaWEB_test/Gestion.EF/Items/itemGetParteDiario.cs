using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    /// <summary>
    /// Item que capta los datos del post para velidar el Parte Diario.
    /// </summary>
    public class itemGetParteDiario
    {
       public int Id { set; get; }
       public int ProyectoId { set; get; }
        public DateTime Fecha_Creacion { set; get; }
        public bool Editar { set; get; }
    }

}
