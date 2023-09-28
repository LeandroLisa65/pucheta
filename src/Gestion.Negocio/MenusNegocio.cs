using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class MenusNegocio
    {
        #region ABM's

        public int Insert(Menus data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Menus data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Menus data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Menus data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Menus.Attach(data);
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

        public List<Menus> Get_All_Menus()
        {
            List<Menus> Lista = new List<Menus>();
            using (var db = new iotdbContext())
            {
                Lista = db.Menus.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }
        public List<Menus> Get_Menus_Padres()
        {
            List<Menus> Lista = new List<Menus>();
            using (var db = new iotdbContext())
            {
                Lista = db.Menus.Where(m => m.IdMenuPadre == 0).OrderBy(o => o.Id).ToList();
            }

            return Lista;          
        }

        public List<Menus> Get_Menus_link()
        {
            List<Menus> Lista = new List<Menus>();
            using (var db = new iotdbContext())
            {
                Lista = db.Menus.Where(m => m.Path != "/").OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }

        public Menus Get_One_Menu(int Id)
        {
            Menus menu = new Menus();
            using (var db = new iotdbContext())
            {
                menu = db.Menus.FirstOrDefault(m => m.Id == Id);
            }

            return menu;
        }

        public List<Menus> Get_Menus(string Tipo)
        {
            List<Menus> Lista = new List<Menus>();
            using (var db = new iotdbContext())
            {
                if (Tipo != "A")
                    Lista = db.Menus.Where(m => m.Tipo == Tipo & m.IdMenuPadre == 0 & m.Activo == true).OrderBy(m => m.Orden).ToList();
                else
                    Lista = db.Menus.Where(m => m.IdMenuPadre == 0 & m.Activo == true).OrderBy(m => m.Orden).ToList();
            }

            return Lista;            
        }

        public List<Menus> Get_Menus_Sub(int IdMenu)
        {
            List<Menus> Lista = new List<Menus>();
            using (var db = new iotdbContext())
            {
                Lista = db.Menus.Where(m => m.IdMenuPadre == IdMenu & m.Activo == true).OrderBy(m => m.Orden).ToList();
            }

            return Lista;
        }

        #endregion
    }
}
