using AulasWebApi.Infra.Db;
using AulasWebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Infra.Repositories
{
    public class RepositoryInDbPostgres<T> : IRepository<T> where T : BaseModel
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private readonly string tablename = typeof(T).Name.ToLower();
        public RepositoryInDbPostgres(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        public int Create(T entity)
        {            
            // Dispose Pattern (Resource Management / RAII)           
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            //Reflection
            var props = entity.GetType().GetProperties();
            // montar a query dinamicamente - via Linq e Reflection
            string columns = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => p.Name.ToLower()));
            string parameters = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => "@" + p.Name.ToLower()));
            var propertiesValues = props.Where(p => p.Name != "Id").ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));
            
            string commandText = $"INSERT INTO {tablename} ({columns}) values({parameters})";
            using NpgsqlCommand insertCommand = new NpgsqlCommand(commandText, connection);
            foreach(var item in propertiesValues)
            {
                insertCommand.Parameters.AddWithValue(item.Key, item.Value);
            }

            insertCommand.ExecuteNonQuery();
            return 0;
        }

        public void Delete(int id)
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = $"DELETE FROM {tablename} WHERE id = @id";
            using NpgsqlCommand deleteCommand = new NpgsqlCommand(commandText, connection);
            deleteCommand.Parameters.AddWithValue("id", id);

            deleteCommand.ExecuteNonQuery();
        }

        public bool Exists(int id)
        {
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();
            string commandText = $"SELECT 1 FROM {tablename} WHERE id = @id LIMIT 1";
            using NpgsqlCommand existsCommand = new NpgsqlCommand(commandText, connection);
            existsCommand.Parameters.AddWithValue("id", id);

            using NpgsqlDataReader dataReader = existsCommand.ExecuteReader();

            return dataReader.Read();
        }

        public List<T> Read()
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename}";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);

            using NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

            List<T> list = new List<T>();
            var props = typeof(T).GetProperties();

            while (dataReader.Read())
            {
                T entity = (T)Activator.CreateInstance(typeof(T));

                foreach(var prop in props)
                {
                    if (dataReader[prop.Name.ToLower()] == null)
                        continue;
                    
                    var colValue = dataReader[prop.Name.ToLower()];
                    if(colValue!= DBNull.Value)
                        prop.SetValue(entity, Convert.ChangeType(colValue, prop.PropertyType) );
                }              
                
                list.Add(entity);
            }

            return list;
        }

        public T ReadById(int id)
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename} WHERE id = @id";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
            selectCommand.Parameters.AddWithValue("id", id);

            using NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

            T entity = (T)Activator.CreateInstance(typeof(T));
            var props = typeof(T).GetProperties();

            if (dataReader.Read())
            {
                foreach (var prop in props)
                {
                    if (dataReader[prop.Name.ToLower()] == null)
                        continue;

                    var colValue = dataReader[prop.Name.ToLower()];
                    if (colValue != DBNull.Value)
                        prop.SetValue(entity, Convert.ChangeType(colValue, prop.PropertyType));
                }               
            }
            return entity;
        }

        public void Update(T entity)
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();
            var props = entity.GetType().GetProperties();

            string setClause = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => $"{p.Name.ToLower()}= @{p.Name.ToLower()}"));

            string commandText = $"UPDATE {tablename} SET {setClause} WHERE id = @id";
            using NpgsqlCommand updateCommand = new NpgsqlCommand(commandText, connection);

            var propertiesValues = props.ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));
            foreach (var item in propertiesValues)
            {
                updateCommand.Parameters.AddWithValue(item.Key, item.Value);
            }

            updateCommand.ExecuteNonQuery();
        }
    }
}
