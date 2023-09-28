using Gestion.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.EF;

namespace Gestion.Web.Components
{
    public class Menu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            string url = HttpContext.Request.Path;
            var ipAddress = HttpContext.Connection.RemoteIpAddress;

            ItemMenuBarra DBarra = new ItemMenuBarra();
            try
            {
                var ident = (HttpContext.User.Identity.Name);
                if (ident != null)
                {
                    string[] us = ident.Split("#");
                    int UsuariosId = Convert.ToInt32(us[0]);
                    DBarra.IdUsuario = UsuariosId;
                    DBarra.NombreApellido = us[1] + " " + us[2];
                    DBarra.Avatar = us[6];
                    DBarra.IP = ipAddress.ToString();


                    List<ItemMenus> menu = new List<ItemMenus>();

                    List<RolesUsuarios> roles = new RolesUsuariosNegocio().GetRolesUsuarios(UsuariosId);

                    int idMPadre = 0;

                    foreach (var itemRoles in roles)
                    {

                        List<AccionesRoles> accines = new AccionesRolesNegocio().Get_AccionesRoles(itemRoles.RolesId);

                        foreach (var itemA in accines)
                        {

                            Acciones accion = new AccionesNegocio().Get_Menu_Acciones(itemA.AccionesID);
                            if (accion != null)
                            {
                                EF.Models.Menus gmenu = new MenusNegocio().Get_One_Menu(accion.MenuId);

                                ItemMenus ms = new ItemMenus();

                                if (gmenu.IdMenuPadre == 0)
                                {
                                    int ExisteMenu = menu.Where(m => m.menu.Nombre == gmenu.Nombre).Count();
                                    foreach (var em in menu)
                                    {
                                        ExisteMenu = ExisteMenu + em.menu_h.Where(m => m.Nombre == gmenu.Nombre).Count();
                                    }

                                    if (ExisteMenu == 0)
                                    {

                                        ItemMenu m1 = new ItemMenu();
                                        m1.Nombre = gmenu.Nombre;
                                        m1.Path = gmenu.Path;
                                        m1.Icon = gmenu.Icon;
                                        m1.Orden = (int)gmenu.Orden;

                                        if (url.ToLower() == gmenu.Path.ToLower())
                                        {
                                            m1.Activo = " active";
                                        }
                                        else
                                        {
                                            m1.Activo = "";
                                        }


                                        ms.menu = m1;

                                        List<ItemMenu> m2 = new List<ItemMenu>();
                                        ms.menu_h = m2;
                                        ms.Orden = (int)gmenu.Orden;

                                        menu.Add(ms);
                                    }
                                }
                                else
                                {
                                    if (idMPadre != (int)gmenu.IdMenuPadre)
                                    {

                                        int ExisteMenu = menu.Where(m => m.menu.Nombre == gmenu.Nombre).Count();

                                        foreach (var em in menu)
                                        {
                                            ExisteMenu = ExisteMenu + em.menu_h.Where(m => m.Nombre == gmenu.Nombre).Count();
                                        }


                                        if (ExisteMenu == 0)
                                        {

                                            EF.Models.Menus gmenuP = new MenusNegocio().Get_One_Menu((int)gmenu.IdMenuPadre);
                                            ItemMenu m0 = new ItemMenu();
                                            m0.Nombre = gmenuP.Nombre;
                                            m0.Path = gmenuP.Path;
                                            m0.Icon = gmenuP.Icon;
                                            m0.Orden = (int)gmenuP.Orden;


                                            List<ItemMenu> m2 = new List<ItemMenu>();
                                            List<EF.Models.Menus> lmenu = new MenusNegocio().Get_Menus_Sub(gmenuP.Id);
                                            bool isActivo = false;
                                            foreach (var itemSM in lmenu)
                                            {
                                                if (new AccionesNegocio().AccionOK(itemRoles.RolesId, itemSM.Id))
                                                {
                                                    ItemMenu mSM = new ItemMenu();
                                                    mSM.Nombre = itemSM.Nombre;
                                                    mSM.Path = itemSM.Path;
                                                    mSM.Icon = itemSM.Icon;
                                                    mSM.Orden = (int)itemSM.Orden;
                                                    if (url.ToLower() == itemSM.Path.ToLower())
                                                    {
                                                        mSM.Activo = " active";
                                                        isActivo = true;
                                                    }
                                                    else
                                                    {
                                                        mSM.Activo = "";
                                                    }

                                                    m2.Add(mSM);
                                                }
                                            }

                                            if (isActivo)
                                            {
                                                m0.Activo = " menu-open";
                                            }
                                            else
                                            {
                                                m0.Activo = "";
                                            }

                                            ms.menu = m0;
                                            ms.menu_h = m2;
                                            ms.Orden = (int)gmenuP.Orden;

                                            menu.Add(ms);
                                        }
                                    }

                                    idMPadre = (int)gmenu.IdMenuPadre;
                                }
                            }
                        }
                    }

                    DBarra.Menu = menu.OrderBy(o => o.Orden).ToList();
                }
                else
                {
                    return View();
                }

            }
            catch (Exception)
            {
                DBarra.Menu = null;
            }

            return View(DBarra);
        }
    }
}
