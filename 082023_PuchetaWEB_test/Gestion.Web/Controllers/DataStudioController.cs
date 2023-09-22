using Gestion.EF;
using Gestion.Negocio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gestion.Web.Controllers
{
    [ApiController]
    [Route("api/DataStudio/{action}/{id?}")]
    public class DataStudioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ReturnData Get_IndicadoresIncidentes()
        {
            ReturnData result = new ReturnData();
            try
            {
                result.data = new DataStudioNegocio().Get_IndicadoresIncidentes().ToList();
                result.isError = false;
            }
            catch (Exception)
            {
                result.isError = true;
                result.data = "Error";
            }
            return result;
        }
        
    }
}
