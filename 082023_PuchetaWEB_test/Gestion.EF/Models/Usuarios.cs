using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gestion.EF.Models
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        [DisplayName("Nombre de Usuario")]
        public string NUsuario { get; set; }
        private string _nombre;

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre {
            get { return _nombre; }
            set {
                _nombre = string.Join(' ', value.Split(' ').Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray());
            }
        }

        private string _apellido;
        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        public string Apellido { 
            get { return _apellido; }
            set
            {
                _apellido = string.Join(' ', value.Split(' ').Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray());
            }
        }



        [Required(ErrorMessage = "El campo Clave es obligatorio")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string Email { get; set; }
        public string Tipo { get; set; }
        public int? Grupo { get; set; }
        public string Avatar { get; set; }
        public string Rsid { get; set; }
        public string Rsn { get; set; }
        public bool Activo { set; get; }
        public bool Borrado { set; get; }
        public DateTime? Fecha { get; set; }

        public List<RolesUsuarios> Roles { get; set; }

        public List<UsuariosDireccion> UsuariosDireccion { get; set; }
        public List<UsuariosDispositivo> UsuariosDispositivo { get; set; }        
        public List<Contratistas> Contratistas { get; set; }

        public virtual string NombreYApellido
        {
            get
            {
                return  Nombre + " " + Apellido;
            }
        }

        public virtual string ApellidoYNombre
        {

            get
            {

                return Apellido + ", " + Nombre;
            }

        }

        public object AsData()
        {
            try
            {
                return new
                {
                    this.Id,
                    this.Nombre,
                    this.Apellido,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Usuarios: AsData(): ");
            }
        }

    }
}
