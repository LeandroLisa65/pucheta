using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public  class ProyectoNegocio
    {

        #region ABM's

        public int Insert(Proyecto data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Proyecto data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Proyecto data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Proyecto data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Proyecto.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        /// <summary>
        /// Método que devuelve una lista de Proyectos con las entidades relacionadas
        /// según indique el parámetro "conInclude"
        /// </summary>
        /// <param name="conIncludes">"True" para incluir entidades relacionadas, de lo contrario "False"</param>
        /// <returns>List de Proyecto</returns>
        public List<Proyecto> Get_All_Proyecto(bool conIncludes)
        {
            try
            {
                List<Proyecto> lProyectos = new List<Proyecto>();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                        lProyectos = db.Proyecto.Include(x => x.Usuarios).ToList();
                    else
                        lProyectos = db.Proyecto.ToList();
                }
                return lProyectos;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyectoNegocio: Get_All_Proyecto(bool): exception.", ex);
            }
        }

        public List<Proyecto> Get_x_Estado(string Estado, bool conIncludes)
        {
            try
            {
                List<Proyecto> lProyectos = new List<Proyecto>();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                        lProyectos = db.Proyecto
                            .Include(x => x.Usuarios)
                            .Where(p => p.Estado == Estado)
                            .ToList();
                    else
                        lProyectos = db.Proyecto
                            .Where(p => p.Estado == Estado || p.Estado == null)
                            .ToList();
                }
                return lProyectos;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyectoNegocio: Get_x_Estado(string): exception.", ex);
            }
        }

        public List<Proyecto> Get_All_Proyecto()
        {
            List<Proyecto> Lista = new List<Proyecto>();
            using (var db = new iotdbContext())
            {
                Lista = db.Proyecto.Include(x=> x.Usuarios).OrderBy(p => p.Nombre).ToList();
            }
            return Lista;
        }

        public List<Proyecto> Get_x_UsuarioId(int UsuarioId)
        {
            try
            {
                List<Proyecto> lProyectos = new List<Proyecto>();
                using (var db = new iotdbContext())
                {
                    lProyectos = db.Proyecto.Where(p => p.UsuariosId == UsuarioId).ToList();
                }
                return lProyectos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyectoNegocio: Get_x_UsuarioId(int): exception.", ex);
            }
        }

        public List<Proyecto> Get_All_Proyecto_Parte_Diario()
        {
            List<Proyecto> Lista = new List<Proyecto>();
            using (var db = new iotdbContext())
            {
                var gLista = db.Proyecto;
                foreach (var item in gLista)
                {
                    int c = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades()
                        .Where(x => x.Proyecto_Ubicaciones.ProyectoId == item.Id).Count();
                    if(c > 0)
                    {
                        Proyecto p = new Proyecto();
                        p = item;
                        Lista.Add(p);
                    }
                }
            }

            return Lista;
        }

        public Proyecto Get_One_Proyecto(int Id)
        {
            Proyecto accion = new Proyecto();
            using (var db = new iotdbContext())
            {
                accion = db.Proyecto.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<Proyecto> Get_By_lIds(List<int> lIdsProyectos)
        {
            List<Proyecto> lProyectos = new List<Proyecto>();
            using (var db = new iotdbContext())
            {
                lProyectos = db.Proyecto
                    .Where(c => lIdsProyectos.Contains(c.Id) == true)
                    .ToList();
            }
            return lProyectos;
        }

        #endregion

    }
}
