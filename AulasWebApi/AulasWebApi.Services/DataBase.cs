using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace AulasWebApi.Services
{
    internal class DataBase
    {
        private readonly string _connectionString = "Host=18.220.9.40;Port=5432;Database=person;Username=postgres;Password=123456";

        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void CloseConnection(NpgsqlConnection connection)
        {
            connection.Close();
        }

    }
}
