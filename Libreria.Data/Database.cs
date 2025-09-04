using Microsoft.Data.SqlClient;
using System.Data;

namespace Libreria.Data
{
    internal class Database
    {
        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlDataReader ExecuteReader(string query, SqlParameter[]? parameters = null)
        {
            var connection = GetConnection();
            using var command = new SqlCommand(query, connection);
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection); // attenzione: il chiamante del reader deve chiudere
        }

        public int ExecuteNonQuery(string sql, SqlParameter[]? parameters = null)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(sql, connection);
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
