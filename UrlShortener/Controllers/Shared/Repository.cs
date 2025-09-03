using MySql.Data.MySqlClient;
using System.Numerics;
using UrlShortener.Models;

namespace UrlShortener.Controllers.Shared
{
    public class Repository
    {
        public Repository()
        {
            
        }

        private MySqlConnection getConnection()
        {
            AppSettings appSettings = new();

            string connectionString = 
                $"SERVER={appSettings.Server};" +
                $"DATABASE={appSettings.Database};" +
                $"UID={appSettings.Uid};" +
                $"PASSWORD={appSettings.Password};";

            MySqlConnection _connection = new(connectionString);
            _connection.Open();
            Console.WriteLine("DB connected");
            return _connection;
        }

        public MySqlDataReader select(string tableName, string? condition = null, int? limit = null)
        {
            string query = $"SELECT * FROM url_shortener.{tableName}";

            if (condition != null) {
                query += $" WHERE {condition}";
            }
            if (limit != null) {
                query += " LIMIT " + limit;
            }

            Console.WriteLine(query);
            var command = new MySqlCommand(query, getConnection());
            MySqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        public int insert(string tableName, Dictionary<string, string> parameters) 
        {
            string sql = $"INSERT INTO {tableName} ";
            sql += "(" + string.Join(", ", parameters.Keys.ToArray().Select(v => $"`{v}`")) + ")";
            sql += " VALUES (" + string.Join(", ", parameters.Values.ToArray().Select(v => $"'{v}'")) + ")";

            var command = new MySqlCommand(sql, getConnection());
            return command.ExecuteNonQuery();
        }

        public int delete(string tableName, int id)
        {
            string sql = $"DELETE FROM {tableName} ";
            sql += $"WHERE `id` = {id}";

            var command = new MySqlCommand(sql, getConnection());
            return command.ExecuteNonQuery();
        }
    }
}
