using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Archivos_Adjuntos_Relacion
    {
        public int Id { get; set; }
        public int Archivos_AdjuntosId { get; set; }
        public Archivos_Adjuntos Archivos_Adjuntos { get; set; }
        /// <summary>
        /// PDA = Parte Diario / INC = Incidentes
        /// </summary>
        [MaxLength(5)]
        public string Entidad { get; set; }
        public int IdEntidad { get; set; }

        [NotMapped]
        public string Nombre
        {
            get
            {
                string nombre = "Sin Nombre";
                try
                {
                    if (this.Archivos_Adjuntos != null)
                    {
                        nombre = this.Archivos_Adjuntos.Archivo;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: Archivos_Adjuntos_Relacion: Nombre: exception.", ex);
                }
                return nombre;
            }
        }

        [NotMapped]
        public string NombreOriginal
        {
            get
            {
                string nombre = "Sin Nombre";
                try
                {
                    if (this.Archivos_Adjuntos != null)
                    {
                        nombre = this.Archivos_Adjuntos.NombreOriginal;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: Archivos_Adjuntos_Relacion: NombreOriginal: exception.", ex);
                }
                return nombre;
            }
        }

        [NotMapped]
        public string Url
        {
            get
            {
                string url = "Sin Url";
                try
                {
                    if (this.Archivos_Adjuntos != null)
                    {
                        url = this.Archivos_Adjuntos.URL;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: Archivos_Adjuntos_Relacion: Url: exception.", ex);
                }
                return url;
            }
        }
    }
}
