using AulasWebApi.Infra.Config;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace AulasWebApi.Infra.Db
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public NpgsqlConnectionFactory(AppConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        // Padrao de projeto Factory Method
        public IDbConnection GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
