using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Core.Models
{
    public class Prestito
    {
        public int Id { get; set; }
        public int IdUtente { get; set; }
        public int IdLibro { get; set; }
        public DateTime Data { get; set; } = DateTime.Now.Date;
    }
}
