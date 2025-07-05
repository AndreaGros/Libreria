using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Core.Models
{
    internal class Utente
    {
        public int Id { get; set; }
        public DateOnly DataNascita { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Mail { get; set; }
    }
}
