using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AulasWebApi.Services
{
    internal class DataBase
    {
        // Padrao de projeto Singleton
        private static readonly Lazy<DataBase> _instance = new Lazy<DataBase>(() => new DataBase());
        private readonly string _connectionString;
        //Propriedade para acessar a instância única
        public static DataBase Instance => _instance.Value;
        private DataBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<DataBase>()
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            this._connectionString = configuration.GetConnectionString("Postgres");
        }

        // Padrao de projeto Factory Method
        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
