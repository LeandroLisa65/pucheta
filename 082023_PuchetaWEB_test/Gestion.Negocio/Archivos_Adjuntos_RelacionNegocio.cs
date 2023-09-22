using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using Gestion.EF;

namespace Gestion.Negocio
{
    public class Archivos_Adjuntos_RelacionNegocio
    {
        #region ABM's

        public int Insert(Archivos_Adjuntos_Relacion data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Archivos_Adjuntos_Relacion data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Archivos_Adjuntos_Relacion data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Archivos_Adjuntos_Relacion data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Archivos_Adjuntos_Relacion.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public List<Archivos_Adjuntos_Relacion> Get_All_Archivos_Adjuntos_Relacion()
        {
            List<Archivos_Adjuntos_Relacion> Lista = new List<Archivos_Adjuntos_Relacion>();
            using (var db = new iotdbContext())
            {
                Lista = db.Archivos_Adjuntos_Relacion.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }

        public Archivos_Adjuntos_Relacion Get_One_Archivos_Adjuntos_Relacion(int Id)
        {
            Archivos_Adjuntos_Relacion accion = new Archivos_Adjuntos_Relacion();
            using (var db = new iotdbContext())
            {
                accion = db.Archivos_Adjuntos_Relacion.FirstOrDefault(m => m.Id == Id);
            }
            return accion;
        }
        public  ReturnData Eliminacion_Archivo_y_Relacion(int Id)
        {

            ReturnData Data = new ReturnData();
            int id = 0;
            Archivos_Adjuntos_Relacion RelacionxArchivo = new Archivos_Adjuntos_Relacion();
            Archivos_Adjuntos Archivo = new Archivos_Adjuntos();
            var db = new iotdbContext();
            var cn2 = new iotdbContext();


            using (db)
            {
                RelacionxArchivo = db.Archivos_Adjuntos_Relacion.FirstOrDefault(m => m.Archivos_AdjuntosId == Id);
                Archivo = db.Archivos_Adjuntos.FirstOrDefault(m => m.Id == Id);
            }

            if (RelacionxArchivo != null)
            {
                try
                {
                    using (var cn = new iotdbContext())
                    {
                        cn.Archivos_Adjuntos_Relacion.Attach(RelacionxArchivo);
                        cn.Entry(RelacionxArchivo).State = EntityState.Deleted;
                        cn.SaveChanges();
                        id = (int)RelacionxArchivo.Id;

                        if(id> 0) 
                        { 
                            cn2.Archivos_Adjuntos.Attach(Archivo);
                            cn2.Entry(Archivo).State = EntityState.Deleted;
                            cn2.SaveChanges();
                            Data.isError = false;
                        }                 
                    }
                }
                catch (Exception)
                {
                    Data.isError = true;
                }
            }


            return Data;

            /*
              try
            {
                using (var db = new iotdbContext())
                {
                    db.Archivos_Adjuntos_Relacion.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception)
            {
                id = 0;
            }
            return id;
             
             */


        }

        public List<Archivos_Adjuntos_Relacion> Get_Archivos_Adjuntos_Relacion(string Entidad, int IdEntidad)
        {
            List<Archivos_Adjuntos_Relacion> Lista = new List<Archivos_Adjuntos_Relacion>();
            using (var db = new iotdbContext())
            {
                Lista = db.Archivos_Adjuntos_Relacion
                    .Include(x => x.Archivos_Adjuntos)
                    .Where(x => x.Entidad == Entidad && x.IdEntidad == IdEntidad)
                    //.Where(x =>  x.IdEntidad == IdEntidad)
                    .ToList();
            }

            return Lista;
        }

        public List<Archivos_Adjuntos_Relacion> Get_x_ArchivoAdjuntoId(int ArchivoAdjuntoId)
        {
            try
            {
                List<Archivos_Adjuntos_Relacion> lAARs = new List<Archivos_Adjuntos_Relacion>();
                using (var db = new iotdbContext())
                {
                    lAARs = db.Archivos_Adjuntos_Relacion
                        .Where(aar => aar.Archivos_AdjuntosId == ArchivoAdjuntoId)
                        .ToList();
                }
                return lAARs;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: Archivos_Adjuntos_RelacionNegocio: Get_x_ArchivoAdjuntoId(int): exception.", ex);
            }
        }

        #endregion
    }
}
