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
    public class NotificacionController : Controller
    {

        #region GETS

        [HttpGet]
        public object GetNotificaciones()
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<Notificacion> lNotificaciones = new NotificacionNegocio()
                        .GetUltimas_x_UsuarioReceptorId(oUsuarioLogueado.Id, true);
                    oResult = new
                    {
                        lNotificaciones = lNotificaciones.Select(n => n.AsData())
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

        [HttpPost]
        public object LeerNotificacion([FromBody] Notificacion oNotificacion)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    Notificacion oNotOriginal = new NotificacionNegocio()
                        .Get_x_Id(oNotificacion.Id, true);
                    if(oNotOriginal.UsuarioReceptorId == oUsuarioLogueado.Id)
                    {
                        if (!oNotificacion.Leida)
                        {
                            oNotOriginal.Estado = ValoresUtiles.Estado_Ntf_Leida;
                            oNotOriginal.FecLectura = DateTime.Now;
                            new NotificacionNegocio().Update(oNotOriginal);
                            oResult = new
                            {
                                oNotificacion = oNotOriginal.AsData()
                            };
                        }
                        else
                        {
                            oResult = new
                            {
                                isError = true,
                                mensaje = "Notificacion ya leida.",
                            };
                        }
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