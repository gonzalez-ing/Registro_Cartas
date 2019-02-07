using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Destinatarios
    {
        [Key]
        public int DestinatarioId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int TotalCartas { get; set; }

        public Destinatarios()
        {
            DestinatarioId = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            TotalCartas = 0;

        }
    }
}
