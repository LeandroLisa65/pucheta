using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace Gestion.Negocio
{
    public class DataStudioNegocio
    {
        public List<itemDTIndicadoresIncidentes> Get_IndicadoresIncidentes()
        {
            List<itemDTIndicadoresIncidentes> Lista = new List<itemDTIndicadoresIncidentes>();
            try
            {
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    var query = "SELECT b.nombre as Nombre,CASE a.EstadoId WHEN 1 THEN 'Abierto' WHEN 2 THEN 'Tratamiento' WHEN 4 THEN 'Validacion' WHEN 50 THEN 'Cerrado' WHEN 99 THEN 'Cancelado' ELSE 'Cancelado' END as Estado ";
                    query = query + ", concat(c.Nombre, c.Apellido) as Creador , ifnull(d.Nombre, 'Sin Proyecto') as Proyecto , a.Creacion_Fecha as F_Creacion, a.Fecha_Cierre as F_Cierre ";
                    var mFrom = " FROM incidentes_historial as a LEFT JOIN proyecto as d on a.ProyectoId = d.Id, incidentes as b, usuarios as c ";
                    var mWhere = " WHERE a.IncidenteId = b.id AND c.id = a.Creacion_UsuarioId LIMIT 10";
                    command.CommandText = query + mFrom + mWhere;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        itemDTIndicadoresIncidentes o = new itemDTIndicadoresIncidentes();

                        o.Nombre = reader.GetString(0);
                        o.Estado = reader.GetString(1);
                        o.Creador = reader.GetString(2);
                        o.Proyecto = reader.GetString(3);
                        DateTime mF_Creacion = reader.GetDateTime(4);
                        DateTime mF_Cierre = reader.GetDateTime(5);

                        //o.F_Creacion = mF_Creacion.ToShortDateString();
                        //o.F_Cierre = mF_Cierre.ToShortDateString();
                        o.F_Creacion = mF_Creacion.ToString("dd-MM-yyyy");
                        o.F_Cierre = mF_Cierre.ToString("dd-MM-yyyy");


                        if (o.Estado == "Cerrado" || o.Estado == "Cancelado")
                            o.Duracion = (mF_Cierre - mF_Creacion).Days;
                        else
                            o.Duracion = (DateTime.Now - mF_Creacion).Days;
                        o.Dias_Abierto = 0;

                        o.Cantidad = 1;

                        Lista.Add(o);

                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            

            return Lista;
        }

        
    }
}
