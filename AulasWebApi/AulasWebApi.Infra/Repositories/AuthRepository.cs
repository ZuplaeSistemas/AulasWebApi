using AulasWebApi.Infra.Db;
using AulasWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Infra.Repositories
{
    public class AuthRepository
    {
        private IDbConnectionFactory _dbConnectionFactory;

        public AuthRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }

        public User GetUserByEmail(string email)
        {
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();
            string commandText = $"select * from user where email = @email";
                     
            using IDbCommand loginCommand = connection.CreateCommand();
            loginCommand.CommandText = commandText;

            IDbDataParameter emailParameter = loginCommand.CreateParameter();
            emailParameter.ParameterName = "email";
            emailParameter.Value = email;
            loginCommand.Parameters.Add(emailParameter);

            using IDataReader dataReader = loginCommand.ExecuteReader();

            User model = null;            
            while (dataReader.Read())
            {
                model = new User();
                model.Email = dataReader["email"].ToString();
                model.Password = dataReader["password"].ToString();
                model.Id = Convert.ToInt32(dataReader["id"]);
                model.Person_Id = Convert.ToInt32(dataReader["person_id"]);
            }
            return model;
        }
    }
}
