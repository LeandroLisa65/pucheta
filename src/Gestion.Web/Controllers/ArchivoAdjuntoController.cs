using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.EF;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Gestion.Web.Controllers
{
    public class ArchivoAdjuntoController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public ArchivoAdjuntoController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ItemLoginUsuario getUsuarioActual()
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

        [HttpPost]
        public object SubirArchivos()
        {
            object oResult = new object();
            try
            {
                ItemLoginUsuario oILUsuario = this.getUsuarioActual();
                List<object> lArchivosAdjuntos = new List<object>();
                List<IFormFile> lFiles = HttpContext.Request.Form.Files.ToList();
                lFiles.ForEach(f =>
                {
                    string[] lComponentesNombre = f.FileName.Split(".");
                    string extension = lComponentesNombre[(lComponentesNombre.Length - 1)];
                    Archivos_Adjuntos oArchivoAdjunto = new Archivos_Adjuntos();
                    Guid oGuid = Guid.NewGuid();
                    var nuevoNombre = DateTime.Now.Year.ToString() +
                        DateTime.Now.Month.ToString() +
                        DateTime.Now.Day.ToString() + "_" +
                        DateTime.Now.Hour + DateTime.Now.Minute +
                        DateTime.Now.Second + "_" +
                        oILUsuario.Id + "_" +
                        oGuid.ToString();
                    oArchivoAdjunto.NombreOriginal = f.FileName;
                    oArchivoAdjunto.Archivo = nuevoNombre + "." + extension;
                    oArchivoAdjunto.Extencion = extension;
                    oArchivoAdjunto.URL = ValoresUtiles.PathArchivos_Temporal;
                    oArchivoAdjunto.Fecha = DateTime.Now;
                    oArchivoAdjunto.IdUsuario = oILUsuario.Id;
                    string path = Path.Combine(_env.WebRootPath, ValoresUtiles.PathArchivos_Temporal);
                    string filePath = Path.Combine(path, oArchivoAdjunto.Archivo);
                    f.CopyTo(new FileStream(filePath, FileMode.Create));
                    int idArchivoAdjunto = new Archivos_AdjuntosNegocio().Insert(oArchivoAdjunto);
                    lArchivosAdjuntos.Add(new
                    {
                        Archivos_AdjuntosId = oArchivoAdjunto.Id,
                        Nombre = oArchivoAdjunto.Archivo,
                        oArchivoAdjunto.NombreOriginal,
                        oArchivoAdjunto.URL
                    });
                });
                oResult = new
                {
                    lArchivosAdjuntos
                };
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    error = true,
                    message = "Error: ArchivoAdjuntoController: SubirArchivo(): exception.",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object EliminarArchivo(Archivos_Adjuntos oArchivoAdjunto)
        {
            object oResult = new object();
            try
            {
                ItemLoginUsuario oILUsuario = this.getUsuarioActual();
                if(oILUsuario.Id > 0)
                {
                    if (oArchivoAdjunto.Id > 0)
                    {
                        oArchivoAdjunto = new Archivos_AdjuntosNegocio()
                            .Get_One_Archivos_Adjuntos(oArchivoAdjunto.Id);
                        Archivos_Adjuntos_RelacionNegocio oAARNegocio = new Archivos_Adjuntos_RelacionNegocio();
                        List<Archivos_Adjuntos_Relacion> lAARs = new Archivos_Adjuntos_RelacionNegocio()
                            .Get_x_ArchivoAdjuntoId(oArchivoAdjunto.Id);
                        lAARs.ForEach(aar => oAARNegocio.Delete(aar));
                        new Archivos_AdjuntosNegocio().Delete(oArchivoAdjunto);
                        oResult = new
                        {
                            message = "Archivo eliminado correctamente."
                        };
                    }
                    else
                    {
                        oResult = new
                        {
                            isError = true,
                            message = "Datos incorrectos."
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
                    message = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

    }
}
