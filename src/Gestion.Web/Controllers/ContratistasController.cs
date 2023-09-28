using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestion.Web.Models;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.EF;

namespace Gestion.Web.Controllers
{
    public class ContratistasController : Controller
    {
        // GET: Contratistas
        public ActionResult Index()
        {
            return View();
        }


        #region Contratistas

        public ActionResult Contratistas()
        {
            return View();
        }

        public ActionResult _ContratistasGrilla()
        {
            List<Contratistas> Lista = new List<Contratistas>();
            Lista = new ContratistasNegocio().Get_All_Contratistas();

            foreach (var item in Lista)
            {
                if (item.RubroId != 0)
                {
                    item.sRubro = new Planificacion_Actividades_RubroNegocio().Get_One_Planificacion_Actividades_Rubro(item.RubroId).Nombre;
                }
                else
                {
                    item.sRubro = "Sin Rubro";
                }
            }

            return PartialView(Lista);
        }

        public ActionResult _ContratistasAbm(int id)
        {
            Contratistas lista = new Contratistas();
            if (id != 0)
            {
                lista = new ContratistasNegocio().Get_One_Contratistas(id);
            }

            return PartialView(lista);
        }

        [HttpPost]
        public ReturnData ContratistasGraba(Contratistas data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new ContratistasNegocio().Update(data);
                }
                else
                {
                    idc = new ContratistasNegocio().Insert(data);
                }

                if (idc > 0)
                {
                    d.isError = false;
                    d.data = "Se han grabado los datos OK.";
                }
                else
                {
                    d.isError = true;
                    d.data = "Error al Grabar en la base.";
                }


            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";

            }

            return d;
        }


        #endregion

        #region Rubro
        [HttpGet]
        public ReturnData GetRubro()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new Planificacion_Actividades_RubroNegocio().Get_All_Planificacion_Actividades_Rubro().ToList();
            return d;
        }
        #endregion
    }
}