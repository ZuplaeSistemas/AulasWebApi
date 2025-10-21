using AulasWebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Services
{
    public class PersonService :  BaseService<Person>
    {
        private DataBase _dataBase;

        public PersonService()
        {
            this._dataBase = DataBase.Instance;
        }

        public override void Create(Person model)
        {
            // Dispose Pattern (Resource Management / RAII)           
            using NpgsqlConnection connection = this._dataBase.GetConnection();

            string commandText = "INSERT INTO person (first_name, last_name, birthdate, created_at) values(@first_name, @last_name, @birthdate, @created_at)";
            using NpgsqlCommand insertCommand = new NpgsqlCommand(commandText, connection);
            insertCommand.Parameters.AddWithValue("first_name", model.FirstName);
            insertCommand.Parameters.AddWithValue("last_name", model.LastName);
            insertCommand.Parameters.AddWithValue("birthdate", model.BirthDate);
            insertCommand.Parameters.AddWithValue("created_at", model.CreatedAt);

            insertCommand.ExecuteNonQuery();        
        }       

        public override void Delete(int id)
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = this._dataBase.GetConnection();

            string commandText = "DELETE FROM person WHERE id = @id";
            using NpgsqlCommand deleteCommand = new NpgsqlCommand(commandText, connection);
            deleteCommand.Parameters.AddWithValue("id", id);

            deleteCommand.ExecuteNonQuery();
        }
        public override void Update(Person model)
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = this._dataBase.GetConnection();

            string commandText = "UPDATE person SET first_name = @first_name, last_name = @last_name, birthdate = @birthdate WHERE id = @id";
            using NpgsqlCommand updateCommand = new NpgsqlCommand(commandText, connection);
            updateCommand.Parameters.AddWithValue("first_name", model.FirstName);
            updateCommand.Parameters.AddWithValue("last_name", model.LastName);
            updateCommand.Parameters.AddWithValue("birthdate", model.BirthDate);
            updateCommand.Parameters.AddWithValue("id", model.Id);

            updateCommand.ExecuteNonQuery();
        }

        public override List<Person> Read()
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = this._dataBase.GetConnection();

            string commandText = "SELECT * FROM person";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);

            using NpgsqlDataReader dataReader = selectCommand.ExecuteReader();
            
            List<Person> list = new List<Person>();

            while (dataReader.Read())
            {
                Person person = new Person();
                person.Id = Convert.ToInt32(dataReader["id"]);
                person.FirstName = dataReader["first_name"].ToString();
                person.LastName = dataReader["last_name"].ToString();
                person.BirthDate = Convert.ToDateTime(dataReader["birthdate"]);
                person.CreatedAt = Convert.ToDateTime(dataReader["created_at"]);
                list.Add(person);
            }

            return list;
        }

        public override Person ReadById(int id)
        {
            // pegar a conexao com o postgreSQL            
            using NpgsqlConnection connection = this._dataBase.GetConnection();

            string commandText = "SELECT * FROM person WHERE id = @id";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
            selectCommand.Parameters.AddWithValue("id", id);

            using NpgsqlDataReader dataReader = selectCommand.ExecuteReader();
            
            Person person = new Person();
            if (dataReader.Read())
            {
                person.Id = Convert.ToInt32(dataReader["id"]);
                person.FirstName = dataReader["first_name"].ToString();
                person.LastName = dataReader["last_name"].ToString();
                person.BirthDate = Convert.ToDateTime(dataReader["birthdate"]);
                person.CreatedAt = Convert.ToDateTime(dataReader["created_at"]);
            }
            return person;
        }        
    }
}
