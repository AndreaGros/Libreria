using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Core.Models
{
    internal class Libro
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public int IdAutore { get; set; }
        public DateOnly Anno { get; set; }
        public int IdPaese { get; set; }
        public int IdLingua { get; set; }
        public decimal Costo { get; set; }
        public int Pagine { get; set; }
    }
}
