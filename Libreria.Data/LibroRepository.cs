using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.Core.Models;

namespace Libreria.Data
{
    public class LibroRepository
    {
        private readonly Database _db;

        public LibroRepository(string connStr)
        {
            _db = new Database(connStr);
        }

        public List<Libro> GetNotAvaiables()
        {
            List < Libro > libri = new List<Libro>();
            string query = "SELECT DISTINCT * FROM Libri l JOIN prestiti p ON l.Id = p.IdLibro;";
            using var reader = _db.ExecuteReader(query);
            while (reader.Read())
            {
                libri.Add(new Libro
                {
                    Id = reader.GetInt32(0),
                    Titolo = reader.GetString(1),
                });
            }
            return libri;
        }
    }
}
