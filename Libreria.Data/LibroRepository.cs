using Libreria.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Data
{
    public class LibroRepository
    {
        private readonly Database _db;

        public LibroRepository(string connStr)
        {
            _db = new Database(connStr);
        }

        public List<Libro> GetAll()
        {
            List<Libro> libri = new List<Libro>();
            string query = "SELECT DISTINCT * FROM Libri";
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
        public Libro? GetById(int idLibro)
        {
            string query = "SELECT Titolo FROM Libri WHERE Id = @idPlaceholder";
            var parameters = new[] { new SqlParameter("@idPlaceholder", idLibro) };
            using var reader = _db.ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Libro
                {
                    Titolo = reader.GetString(0),
                };
            }
            return null;
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
