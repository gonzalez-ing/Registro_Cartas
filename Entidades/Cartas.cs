using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Cartas
    {
        [Key]
        public int CartaId { get; set; }
        public DateTime Fecha { get; set; }
        public int DestinatarioId { get; set; }
        public string Cuerpo { get; set; }

        public Cartas()
        {
            CartaId = 0;
            Fecha = DateTime.Now;
            DestinatarioId = 0;
            Cuerpo = string.Empty;
           
        }
    }
}
