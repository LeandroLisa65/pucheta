using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
    public class AccionesNegocio
    {
        #region ABM's

        public int Insert(Acciones data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Acciones data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Acciones data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Acciones data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Acciones.Attach(data);
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

        public List<Acciones> Get_All_Acciones()
        {
            List<Acciones> Lista = new List<Acciones>();
            using (var db = new iotdbContext())
            {
                Lista = db.Acciones.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }


        public Acciones Get_One_Acciones(int Id)
        {
            Acciones accion = new Acciones();
            using (var db = new iotdbContext())
            {
                accion = db.Acciones.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public Acciones Get_Menu_Acciones(int Id)
        {
            Acciones accion = new Acciones();
            using (var db = new iotdbContext())
            {
                accion = db.Acciones.FirstOrDefault(m => m.Id == Id && m.Activo == true && m.MenuId > 0);
            }

            return accion;
        }


        public bool AccionOK(int RolId, int MenuId)
        {
            bool ok = false;
            using (var db = new iotdbContext())
            {
                try
                {
                    var result = (from A in db.Acciones
                                join AR in db.AccionesRoles on A.Id equals AR.AccionesID
                                where AR.RolesId == RolId && A.MenuId == MenuId
                                select (new { id = A.MenuId })).FirstOrDefault();
                    int exit = 0;
                    if (result != null)
                        exit = result.id;

                    ok = true;
                }
                catch (Exception)
                {
                    ok = false;
                } 
                
            }

            return ok;
        }

        public List<Acciones> Get_By_lIds(List<int> lIdsAcciones)
        {
            List<Acciones> lAcciones = new List<Acciones>();
            using (var db = new iotdbContext())
            {
                lAcciones = db.Acciones
                    .Where(c => lIdsAcciones.Contains(c.Id) == true)
                    .ToList();
            }
            return lAcciones;
        }

        #endregion
    }
}
