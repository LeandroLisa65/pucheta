using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class Notificacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public int UsuarioEmisorId { get; set; }
        public Usuarios UsuarioEmisor { get; set; }
        public DateTime FecEmision { get; set; }
        public int UsuarioReceptorId { get; set; }
        public Usuarios UsuarioReceptor { get; set; }
        public string Estado { get; set; }
        public DateTime FecLectura { get; set; }
        public int EntidadId { get; set; }
        public string EntidadNombre { get; set; }

        #region PROPIEDADES DERIVADAS

        public string FechaEmision
        {
            get
            {
                return this.FecEmision.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }
        public string FechaLectura
        {
            get
            {
                return this.FecLectura.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }
        public bool Leida
        {
            get
            {
                return this.Estado == ValoresUtiles.Estado_Ntf_Leida;
            }
        }

        public object AsData()
        {
            try
            {
                object oData = new object();
                object oDUsuarioEmisor = new object();
                if (this.UsuarioEmisor != null) 
                    oDUsuarioEmisor = this.UsuarioEmisor.AsData();
                object oDUsuarioReceptor = new object();
                if (this.UsuarioReceptor != null) 
                    oDUsuarioReceptor = this.UsuarioReceptor.AsData();
                return new
                {
                    this.Id,
                    this.Mensaje,
                    UsuarioEmisor = oDUsuarioEmisor,
                    this.FechaEmision,
                    UsuarioReceptor = oDUsuarioReceptor,
                    this.FechaLectura,
                    this.Estado,
                    this.Leida,
                    this.EntidadId,
                    this.EntidadNombre
                };
            }
            catch(Exception ex)
            {
                throw new Exception("Error: Notificacion: AsData(): exception.", ex);
            }
        }

        #endregion
    }
}
