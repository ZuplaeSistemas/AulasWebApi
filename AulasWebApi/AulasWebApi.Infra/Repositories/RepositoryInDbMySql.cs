using AulasWebApi.Infra.Db;
using AulasWebApi.Models;
using MySql.Data.MySqlClient;

namespace AulasWebApi.Infra.Repositories
{
    public class RepositoryInDbMySql<T> : RepositoryInDb<T> where T : BaseModel
    {        
        public RepositoryInDbMySql(IDbConnectionFactory dbConnectionFactory) :  base(dbConnectionFactory)
        {
        }
    }
}
