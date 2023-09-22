using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Negocio
{
    /// <summary>
    /// new EnviaEmailNegocio("obustamante@iotecnologias.com.ar", "Anuncio", "Mensaje del mail", true);
    /// </summary>
    public static class EnviaEmailNegocio
    {
        public static bool Enviar(string EmailsDestino, string Asunto, string Mensaje, bool Html)
        {
            try
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);
                var configuration = builder.Build();
                string connection = configuration["SiteSettings:AdminEmail"];

                bool envio;
                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                //email.From = new MailAddress(configuration["setEmail:Credentials:user"]);
                email.From = new MailAddress(ValoresUtiles.EmailSeguimientoObra); // new MailAddress(configuration["setEmail:Credentials:user"]);
                email.Subject = Asunto + " (" + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + ")";
                email.SubjectEncoding = System.Text.Encoding.UTF8;
                email.Body = Mensaje;
                email.IsBodyHtml = Html;
                email.Priority = MailPriority.Normal;

                smtp.Host = configuration["setEmail:host"];
                smtp.Port = Convert.ToInt32(configuration["setEmail:port"]);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                
                smtp.Credentials = new NetworkCredential(
                    configuration["setEmail:Credentials:user"], configuration["setEmail:Credentials:pass"]);
                smtp.Timeout = 20000;

                bool EnviaEmail = Convert.ToBoolean(configuration["EnviaEmail"]);
                if (EnviaEmail)
                {
                    var mails = EmailsDestino.Trim().Split(';');
                    foreach (string dir in mails)
                        email.To.Add(dir);
                }
                else email.To.Add(ValoresUtiles.EmailTesting);
                Email oEmail = new Email();
                oEmail.Asunto = Asunto;
                oEmail.Mensaje = Mensaje;
                oEmail.Remitente = ValoresUtiles.EmailSeguimientoObra;
                oEmail.Destinatarios = EmailsDestino;
                oEmail.FecCreacion = DateTime.Now;
                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    envio = true;
                    oEmail.Enviado = true;
                }
                catch (Exception ex)
                {
                    envio = false;
                    oEmail.Enviado = false;
                }
                new EmailNegocio().Insert(oEmail);
                return envio;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: EnviaEmailNegocio: Enviar(string, string, string, bool): exception.", ex);
            }
        }

        public static async Task<bool> EnviarEmail_Async(string EmailsDestino, string Asunto, string Mensaje, bool Html)
        {
            try
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);
                var configuration = builder.Build();
                string connection = configuration["SiteSettings:AdminEmail"];

                bool envio;
                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                //email.From = new MailAddress(configuration["setEmail:Credentials:user"]);
                email.From = new MailAddress(ValoresUtiles.EmailSeguimientoObra); // new MailAddress(configuration["setEmail:Credentials:user"]);
                email.Subject = Asunto + " (" + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + ")";
                email.SubjectEncoding = System.Text.Encoding.UTF8;
                email.Body = Mensaje;
                email.IsBodyHtml = Html;
                email.Priority = MailPriority.Normal;

                smtp.Host = configuration["setEmail:host"];
                smtp.Port = Convert.ToInt32(configuration["setEmail:port"]);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential(configuration["setEmail:Credentials:user"], configuration["setEmail:Credentials:pass"]);
                smtp.Timeout = 20000;

                bool EnviaEmail = Convert.ToBoolean(configuration["EnviaEmail"]);
                if (EnviaEmail)
                {
                    var mails = EmailsDestino.Trim().Split(';');
                    foreach (string dir in mails)
                        email.To.Add(dir);
                }
                else email.To.Add(ValoresUtiles.EmailTesting);
                Email oEmail = new Email();
                oEmail.Asunto = Asunto;
                oEmail.Mensaje = Mensaje;
                oEmail.Remitente = ValoresUtiles.EmailSeguimientoObra;
                oEmail.Destinatarios = EmailsDestino;
                oEmail.FecCreacion = DateTime.Now;
                try
                {
                    await smtp.SendMailAsync(email);
                    email.Dispose();
                    envio = true;
                    oEmail.Enviado = true;
                }
                catch
                {
                    oEmail.Enviado = false;
                    envio = false;
                }
                new EmailNegocio().Insert(oEmail);
                return envio;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: EnviaEmailNegocio: EnviarEmail_Async(string, string, string, bool): exception.", ex);
            }
        }

    }
}
