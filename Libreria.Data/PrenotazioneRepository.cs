using Libreria.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Data
{
    public class PrenotazioneRepository
    {
        private readonly Database _db;

        public PrenotazioneRepository(string connStr)
        {
            _db = new Database(connStr);
        }

        public List<Prenotazione> GetAll()
        {
            var prenotazioni = new List<Prenotazione>();
            string query = "SELECT p.Id AS id_prenotazione, l.Titolo AS nome_libro, p.IdUtente AS id_utente, p.IdLibro AS id_libro FROM Prenotazioni p JOIN Libri l ON p.IdLibro = l.Id;";
            using var reader = _db.ExecuteReader(query);
            while (reader.Read())
            {
                prenotazioni.Add(new Prenotazione
                {
                    Id = reader.GetInt32(0),
                    LibroPrenotato = reader.GetString(1),
                    IdUtente = reader.GetInt32(2),
                    IdLibro = reader.GetInt32(3)
                });
            }
            return prenotazioni;
        }

        public int Add(Prenotazione prenotazione)
        {
            string query = "INSERT INTO Prenotazioni (IdUtente, IdLibro) VALUES(@IdUtentePlaceholder, @IdLibroPlaceholder)";
            var parameters = new[]
            {
                new SqlParameter("@IdUtentePlaceholder", prenotazione.IdUtente),
                new SqlParameter("@IdLibroPlaceholder", prenotazione.IdLibro),
            };
            return _db.ExecuteNonQuery(query, parameters);
        }
    }
}
