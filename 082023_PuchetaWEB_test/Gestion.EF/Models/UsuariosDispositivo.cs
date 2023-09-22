using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;

namespace Gestion.EF.Models
{
    public class UsuariosDispositivo
    {
        public int Id { set; get; }
        public int UsuariosId { set; get; }
        public int DispositivosId { set; get; }
        [MaxLength(50)]
        public string Numero { set; get; }
        [MaxLength(250)]
        public string IMEI { set; get; }
        public string FINGERTMP { set; get; }
        public string ImageBase64 { set; get; }        
        [MaxLength(50)]
        public string OS { set; get; }
        [MaxLength(50)]
        public string Empresa { set; get; }
        public bool Activo { set; get; }
        public bool Borrado { set; get; }

        [NotMapped]
        public string nDispositivos {
            set { }
            get {
                string ndis = "";
                using (var db = new iotdbContext())
                {
                    ndis = db.Dispositivos.FirstOrDefault(d => d.Id == DispositivosId).Detalle;
                }

                return ndis;
            }
        }
    }
}
