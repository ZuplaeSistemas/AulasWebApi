using AulasWebApi.Infra.Db;
using AulasWebApi.Models;
using Npgsql;

namespace AulasWebApi.Infra.Repositories
{
    public class RepositoryInDbPostgres<T> : RepositoryInDb<T> where T : BaseModel
    {
        public RepositoryInDbPostgres(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }        
    }
}
