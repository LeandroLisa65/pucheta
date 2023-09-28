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
    public class IndicesController : Controller
    {

        #region Indices
        // GET: Indices
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _IndicesGrilla()
        {
            List<Indices> Lista = new List<Indices>();
            Lista = new IndicesNegocio().Get_All_Indices();

            return PartialView(Lista);
        }

        public ActionResult _IndicesAbm(int id)
        {
            Indices lista = new Indices();
            try
            {
                if (id != 0)
                {
                    lista = new IndicesNegocio().Get_One_indices(id, true);
                }

                return PartialView(lista);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        [HttpPost]
        public ReturnData IndicesGraba(Indices data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new IndicesNegocio().Update(data);
                }
                else
                {
                    idc = new IndicesNegocio().Insert(data);
                }

                if (idc > 0)
                {
                    d.isError = false;
                    d.data = "Se han grabado los datos OK.";
                    d.data1 = idc;
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

        #region Indices Valores
        [HttpPost]
        public ReturnData CargoIndice_ValoresGrilla(ItemSelectList pIdIndice)
        {
            ReturnData d = new ReturnData();

            if (pIdIndice.Value != "0")
            {
                long mId = Convert.ToInt64(pIdIndice.Value);
                List<Indice_Valores> lst = new Indice_ValoresNegocio().Get_All_Indice_Valores().Where(p => p.IdIndice == mId).OrderByDescending(p => p.Ano).ThenByDescending(p => p.Mes).ToList();
                d.data = lst;
            }
            else
            {
                d.isError = true;
                d.data = "Se produjo un error al buscar los Valores del Indice";
            }
            return d;
        }

        [HttpPost]
        public ReturnData IndicesValoresGraba(Indice_Valores data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                //Valido que no existe un valor para ese indice, mes y año
                List<Indice_Valores> lst = new Indice_ValoresNegocio().Get_All_Indice_Valores().Where(p => p.IdIndice == data.IdIndice && p.Mes == data.Mes && p.Ano == p.Ano).ToList();
                if (lst.Count > 0)
                {
                    d.isError = true;
                    d.data = "Ya existe un valor para este indice en el mes:" + data.Mes +" y año:" + data.Ano +".";
                    return d;
                }

                int idc;
                if (data.Id > 0)
                {
                    idc = new Indice_ValoresNegocio().Update(data);
                }
                else
                {
                    idc = new Indice_ValoresNegocio().Insert(data);
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

        [HttpPost]
        public ReturnData BorrarIndice_Valores(ItemSelectList pIdIndice)
        {
            ReturnData d = new ReturnData();

            if (pIdIndice.Value != "0")
            {
                long mId = Convert.ToInt64(pIdIndice.Value);

                Indice_Valores oObj = new Indice_ValoresNegocio().Get_One_Indice_Valores(mId);
                var m = new Indice_ValoresNegocio().Delete(oObj);
                d.data = "ok";
            }
            else
            {
                d.isError = true;
                d.data = "Se produjo un error al buscar los Valores del Indice";
            }
            return d;
        }
        #endregion

    }
}
