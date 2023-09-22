using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;

namespace Gestion.EF.Models
{
    public class UsuariosDireccion
    {
        public int Id { set; get; }
        public int UsuariosId { set; get; }
        public int ProvinciasId { set; get; }
        public int LocalidadesId { set; get; }
        [MaxLength(250)]
        public string Barrio { set; get; }

        [MaxLength(250)] 
        public string Calle { set; get; }
        [MaxLength(50)]
        public string Altura { set; get; }
        [MaxLength(50)]
        public string Piso { set; get; }
        [MaxLength(50)]
        public string Dpto { set; get; }
        [MaxLength(50)]
        public string CP { set; get; }
        public bool Activo { set; get; }
        public bool Borrado { set; get; }

        [NotMapped]
        public string nProvincias
        {
            set { }
            get
            {
                string nprov = "";
                using (var db = new iotdbContext())
                {
                    nprov = db.Provincias.FirstOrDefault(d => d.Id == ProvinciasId).Provincia;
                }

                return nprov;
            }
        }

        [NotMapped]
        public string nLocalidades
        {
            set { }
            get
            {
                string nloc = "";
                using (var db = new iotdbContext())
                {
                    nloc = db.Localidades.FirstOrDefault(d => d.Id == LocalidadesId).Localidad;
                }

                return nloc;
            }
        }
    }
}
