using Gestion.EF;
using Gestion.EF.Models;
using Gestion.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion.Web.Controllers
{
    public class PartidaPresupuestariaController : Controller
    {

        #region GETS

        [HttpGet]
        public object GetAll()
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<PartidaPresupuestaria> lPartidasPresupuestarias = new PartidaPresupuestariaNegocio().GetAll();
                    oResult = new
                    {
                        lPartidasPresupuestarias
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region POTS

        #endregion

        #region PRIVADOS

        #endregion

        #region USUARIO LOGUEADO

        private ItemLoginUsuario getItemLoguinUsuarioActual()
        {
            ItemLoginUsuario d = new ItemLoginUsuario();
            string[] us = (HttpContext.User.Identity.Name).Split("#");

            d.Id = Convert.ToInt32(us[0]);
            d.Nombre = us[1];
            d.Apellido = us[2];
            d.Email = us[3];
            d.Tipo = us[4];
            d.ApellidoNombre = us[1] + ", " + us[2];
            d.Grupo = Convert.ToInt32(us[5]);

            return d;
        }

        private Usuarios getUsuarioActual()
        {
            Usuarios oUsuario = new Usuarios();
            try
            {
                ItemLoginUsuario oILUsuario = this.getItemLoguinUsuarioActual();
                oUsuario = new UsuariosNegocio().Get_Usuario(oILUsuario.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: getUsuarioActual(): exception.", ex);
            }
            return oUsuario;
        }

        #endregion

    }
}