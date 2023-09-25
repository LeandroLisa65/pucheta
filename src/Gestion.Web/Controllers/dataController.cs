using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF;
using Gestion.Negocio;
using Gestion.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestion.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class dataController : Controller
    {
       

        [HttpPost]
        [ActionName("Get_encrip")]
        public ReturnData Get_encrip([FromBody]JObject obj)
        {
            ReturnData dReturn = new ReturnData();
           /* try
            {
                string encript = "";
                if (gData.ncadena == 0)
                {
                    encript = Security.Encrypt(gData.scadena);
                }
                else
                {
                    encript = Security.Decrypt(gData.scadena);
                }

                dReturn.isError = false;
                dReturn.data = encript;
            }
            catch (Exception)
            {
                dReturn.isError = true;
                dReturn.data = "Error;";
            }*/


            return dReturn;

        }


    }
}
