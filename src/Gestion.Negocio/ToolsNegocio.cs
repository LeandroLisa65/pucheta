using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.Negocio
{
    public static class ToolsNegocio
    {

        public static string CrearPassword(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890#¡%@";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }
        public static string CrearAlfaNumerico(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        public static string EliminaEnter(string data)
        {
            var dec = "";
            if (!string.IsNullOrEmpty(data))
            {
                dec = data.Replace("\n", " ");
                dec = dec.Replace("\r", " ");
                dec = dec.Replace("\\", "/");
            }
            return dec;
        }

        //private ItemLoginUsuario UsuarioLogin()
        //{
        //    ItemLoginUsuario d = new ItemLoginUsuario();
        //    string[] us = (HttpContext.User.Identity.Name).Split("#");

        //    d.Id = Convert.ToInt32(us[0]);
        //    d.Nombre = us[1];
        //    d.Apellido = us[2];
        //    d.Email = us[3];
        //    d.Tipo = us[4];
        //    d.Grupo = Convert.ToInt32(us[5]);

        //    return d;
        //}
    }
}
