using AulasWebApi.Infra.Db;
using AulasWebApi.Models;
using Npgsql;
using System.Data;


namespace AulasWebApi.Infra.Repositories
{
    public class RepositoryInDb<T> : IRepository<T> where T : BaseModel
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private readonly string tablename = typeof(T).Name.ToLower();

        public RepositoryInDb(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        private IDbCommand CreateCommand(IDbConnection connection, string commandText)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }
        public int Create(T entity)
        {
            // Dispose Pattern (Resource Management / RAII)           
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            //Reflection
            var props = entity.GetType().GetProperties();
            // montar a query dinamicamente - via Linq e Reflection
            string columns = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => p.Name.ToLower()));
            string parameters = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => "@" + p.Name.ToLower()));
            var propertiesValues = props.Where(p => p.Name != "Id").ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));

            string commandText = $"INSERT INTO {tablename} ({columns}) values({parameters})";
            IDbCommand insertCommand = CreateCommand(connection, commandText);
            
            foreach (var item in propertiesValues)
            {
                IDbDataParameter parameter = insertCommand.CreateParameter();
                parameter.ParameterName = item.Key;
                parameter.Value = item.Value;
                insertCommand.Parameters.Add(parameter);
            }

            insertCommand.ExecuteNonQuery();
            return 0;
        }

        public void Delete(int id)
        {
            // pegar a conexao com o postgreSQL            
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            string commandText = $"DELETE FROM {tablename} WHERE id = @id";
            using IDbCommand deleteCommand = CreateCommand(connection, commandText);

            IDbDataParameter parameter = deleteCommand.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;

            deleteCommand.ExecuteNonQuery();
        }

        public bool Exists(int id)
        {
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();
            string commandText = $"SELECT 1 FROM {tablename} WHERE id = @id LIMIT 1";

            using IDbCommand existsCommand = CreateCommand(connection, commandText);

            IDbDataParameter parameter = existsCommand.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;

            using IDataReader dataReader = existsCommand.ExecuteReader();

            return dataReader.Read();
        }

        public List<T> Read()
        {
            // pegar a conexao com o postgreSQL            
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename}";
            using IDbCommand selectCommand = CreateCommand(connection, commandText);

            using IDataReader dataReader = selectCommand.ExecuteReader();

            List<T> list = new List<T>();
            var props = typeof(T).GetProperties();

            while (dataReader.Read())
            {
                T entity = (T)Activator.CreateInstance(typeof(T));

                foreach (var prop in props)
                {
                    if (dataReader[prop.Name.ToLower()] == null)
                        continue;

                    var colValue = dataReader[prop.Name.ToLower()];
                    if (colValue != DBNull.Value)
                        prop.SetValue(entity, Convert.ChangeType(colValue, prop.PropertyType));
                }

                list.Add(entity);
            }

            return list;
        }

        public T ReadById(int id)
        {
            // pegar a conexao com o postgreSQL            
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename} WHERE id = @id";
            using IDbCommand selectCommand = CreateCommand(connection, commandText);

            IDbDataParameter parameter = selectCommand.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;

            using IDataReader dataReader = selectCommand.ExecuteReader();

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
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();
            var props = entity.GetType().GetProperties();

            string setClause = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => $"{p.Name.ToLower()}= @{p.Name.ToLower()}"));

            string commandText = $"UPDATE {tablename} SET {setClause} WHERE id = @id";
            using IDbCommand updateCommand = CreateCommand(connection, commandText);

            var propertiesValues = props.ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));
            foreach (var item in propertiesValues)
            {
                IDbDataParameter parameter = updateCommand.CreateParameter();
                parameter.ParameterName = item.Key;
                parameter.Value = item.Value;
                updateCommand.Parameters.Add(parameter);
            }

            updateCommand.ExecuteNonQuery();
        }
    }
}
