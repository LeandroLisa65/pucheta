using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Gestion.EF;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gestion.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (HttpContext.User.Identity.Name != null)
                {
                    string[] us = (HttpContext.User.Identity.Name).Split("#");
                    return RedirectToAction("Index", "Gestion");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }

        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(LoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                Usuarios user = new UsuariosNegocio().Get_Usuario_Login(model.Input.NUsuario, Security.Encrypt(model.Input.Password));
                if (user != null)
                {
                    string dataKey = user.Id + "#" + user.Nombre + "#" + user.Apellido + "#" + user.Email + "#" + user.Tipo + "#" + user.Grupo + "#" + user.Avatar;
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dataKey)                     
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "logining");

                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(userIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = model.Input.RememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddHours(72)
                    });


                    new UsuariosLogNegocio().Insert(user.Id, "Login", 9999, ipAddress.ToString());

                    return RedirectToAction("Index", "Gestion");
                }
                else
                {
                    new UsuariosLogNegocio().Insert(0, "Login Fail / " + JsonConvert.SerializeObject(model), 9990, ipAddress.ToString());
                    // Logger.Error("Login Fail " + JsonConvert.SerializeObject(model));
                    TempData["UserLoginFailed"] = "Login Error. Ingrese datos validos.";
                    return View();
                }
            }
            return View(model);
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
            d.ApellidoNombre = us[2] + ", " + us[1];
            d.Grupo = Convert.ToInt32(us[5]);

            return d;
        }
    }
}