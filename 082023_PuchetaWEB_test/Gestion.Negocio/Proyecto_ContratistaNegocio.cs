using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
     public class Proyecto_ContratistaNegocio
    {

        #region ABM's

        public int Insert(Proyecto_Contratista data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Proyecto_Contratista data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Proyecto_Contratista data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Proyecto_Contratista data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Proyecto_Contratista.Attach(data);
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

        public List<Proyecto_Contratista> Get_All_Proyecto_Contratista(int IdProyecto)
        {
            List<Proyecto_Contratista> Lista = new List<Proyecto_Contratista>();
            using (var db = new iotdbContext())
            {
                Lista = db.Proyecto_Contratista.Where(x=> x.ProyectoId == IdProyecto).ToList();
            }

            return Lista;
        }

        public Proyecto_Contratista Get_One_Proyecto_Contratista(int Id)
        {
            Proyecto_Contratista accion = new Proyecto_Contratista();
            using (var db = new iotdbContext())
            {
                accion = db.Proyecto_Contratista
                    .FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<Proyecto_Contratista> Get_x_IdContratista(int IdContratista, bool conInclude)
        {
            try
            {
                List<Proyecto_Contratista> lProyectosContratistas = new List<Proyecto_Contratista>();
                using (var db = new iotdbContext())
                {
                    if(conInclude)
                        lProyectosContratistas = db.Proyecto_Contratista
                            .Include(pc => pc.Proyecto)
                            .Include(pc => pc.Contratista)
                            .Where(pc => IdContratista == 0 || pc.ContratistaId == IdContratista).ToList();
                    else
                        lProyectosContratistas = db.Proyecto_Contratista
                            .Where(pc => pc.ContratistaId == IdContratista).ToList();
                }
                return lProyectosContratistas;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: Proyecto_ContratistaNegocio: Get_x_IdContratista(int): exception.", ex);
            }
        }

        #endregion



    }
}
