using Libreria.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Data
{
    public class PrestitoRepository
    {
        private readonly Database _db;

        public PrestitoRepository(string connStr)
        {
            _db = new Database(connStr);
        }
        public int Add(Prestito prestito)
        {
            string query = "INSERT INTO Prestiti (IdUtente, IdLibro, Data) VALUES(@IdUtentePlaceholder, @IdLibroPlaceholder, @DataPlaceholder)";
            var parameters = new[]
            {
                new SqlParameter("@IdUtentePlaceholder", prestito.IdUtente),
                new SqlParameter("@IdLibroPlaceholder", prestito.IdLibro),
                new SqlParameter("@DataPlaceholder", prestito.Data)
            };
            return _db.ExecuteNonQuery(query, parameters);
        }

        public int CountUser(int id)
        {
            string query = "SELECT * FROM Prestiti WHERE IdUtente = @IdPlaceholder;";
            var parameters = new[]
            {
                new SqlParameter("@IdPlaceholder", id)
            };
            using var reader = _db.ExecuteReader(query, parameters);
            int count = 0;
            while (reader.Read())
                count++;
            return count;
        }

        public List<Prestito> GetPrestitiScaduti()
        {
            string query = @"
        SELECT Id, IdUtente, IdLibro, Data
        FROM Prestiti
        WHERE DATEDIFF(DAY, Data, GETDATE()) >= 90;";

            var prestiti = new List<Prestito>();

            using var reader = _db.ExecuteReader(query);
            while (reader.Read())
            {
                prestiti.Add(new Prestito
                {
                    Id = reader.GetInt32(0),
                    IdUtente = reader.GetInt32(1),
                    IdLibro = reader.GetInt32(2),
                    Data = reader.GetDateTime(3)
                });
            }

            return prestiti;
        }
    }
}
