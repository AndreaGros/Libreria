using Libreria.Core.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Libreria.Data
{
    public class UtenteRepository
    {
        private readonly Database _db;

        public UtenteRepository(string connStr)
        {
            _db = new Database(connStr);
        }

        // Recupera tutti gli utenti
        public List<Utente> GetAll()
        {
            var utenti = new List<Utente>();
            string query = "SELECT Id, DataNascita, Nome, Cognome, Mail FROM Utenti";
            using var reader = _db.ExecuteReader(query);
            while (reader.Read())
            {
                utenti.Add(new Utente
                {
                    Id = reader.GetInt32(0),
                    DataNascita = reader.GetDateTime(1),
                    Nome = reader.GetString(2),
                    Cognome = reader.GetString(3),
                    Mail = reader.GetString(4)
                });
            }
            return utenti;
        }

        // Recupera un utente per Id
        public Utente? GetById(int idUtente)
        {
            string query = "SELECT Id, DataNascita, Nome, Cognome, Mail FROM Utenti WHERE Id = @idPlaceholder";
            var parameters = new[] { new SqlParameter("@idPlaceholder", idUtente) };
            using var reader = _db.ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Utente
                {
                    Id = reader.GetInt32(0),
                    DataNascita = reader.GetDateTime(1),
                    Nome = reader.GetString(2),
                    Cognome = reader.GetString(3),
                    Mail = reader.GetString(4)
                };
            }
            return null;
        }

        // Aggiunge un nuovo utente
        public int Add(Utente utente)
        {
            string query = "INSERT INTO Utenti (DataNascita, Nome, Cognome, Mail) VALUES(@dataNascitaPlaceholder, @nomePlaceholder, @cognomePlaceholder, @mailPlaceholder)";
            var parameters = new[]
            {
                new SqlParameter("@dataNascitaPlaceholder", utente.DataNascita),
                new SqlParameter("@nomePlaceholder", utente.Nome),
                new SqlParameter("@cognomePlaceholder", utente.Cognome),
                new SqlParameter("@mailPlaceholder", utente.Mail)
            };
            return _db.ExecuteNonQuery(query, parameters);
        }

        // Aggiorna un utente esistente
        public int Update(Utente utente)
        {
            string query = "UPDATE Utenti SET DataNascita=@dataNascitaPlaceholder, Nome=@nomePlaceholder, Cognome=@cognomePlaceholder, Mail=@mailPlaceholder WHERE Id=@idPlaceholder";
            var parameters = new[]
            {
                new SqlParameter("@idPlaceholder", utente.Id),
                new SqlParameter("@dataNascitaPlaceholder", utente.DataNascita),
                new SqlParameter("@nomePlaceholder", utente.Nome),
                new SqlParameter("@cognomePlaceholder", utente.Cognome),
                new SqlParameter("@mailPlaceholder", utente.Mail)
            };
            return _db.ExecuteNonQuery(query, parameters);
        }

        // Elimina un utente per Id
        public int Delete(int idUtente)
        {
            string query = "DELETE FROM Utenti WHERE Id=@idPlaceholder";
            var parameters = new[]
            {
                new SqlParameter("@idPlaceholder", idUtente)
            };
            return _db.ExecuteNonQuery(query, parameters);
        }
    }
}