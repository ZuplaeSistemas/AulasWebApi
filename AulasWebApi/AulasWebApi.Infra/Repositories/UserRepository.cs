using AulasWebApi.Infra.Db;
using AulasWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Infra.Repositories
{
    public class UserRepository : RepositoryInDbMySql<User>
    {
        public UserRepository(IDbConnectionFactory factory) : base(factory)
        {
            
        }
    }
}
