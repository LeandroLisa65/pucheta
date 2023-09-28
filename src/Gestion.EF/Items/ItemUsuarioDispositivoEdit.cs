using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF
{
    public class ItemUsuarioDispositivoEdit
    {
        public int Id { set; get; }
        public int UsuariosId { set; get; }
        public int DispositivosId { set; get; }
        public string Numero { set; get; }
        public string IMEI { set; get; }
        public string OS { set; get; }
        public string Empresa { set; get; }
        public bool Activo { set; get; }
        public bool Borrado { set; get; }
    }
}
