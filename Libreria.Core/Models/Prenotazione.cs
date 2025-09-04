using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Core.Models
{
    public class Prenotazione
    {
        public int Id { get; set; }
        public int IdUtente { get; set; }
        public int IdLibro { get; set; }

        public string? LibroPrenotato { get; set; }
    }
}
