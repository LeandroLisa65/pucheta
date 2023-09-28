using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Gestion.Web.Models
{
    public class UserDataRequestContext
    {
        public object Response { get; private set; }

        public string getKey()
        {
            string dataUser = "";
            try
            {
                
            }
            catch (Exception)
            {
                dataUser = "ERROR";
            }

            return dataUser;
        }
     
    }
}
