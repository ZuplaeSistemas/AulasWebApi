using AulasWebApi.Infra.Db;
using AulasWebApi.Infra.Repositories;
using AulasWebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Services
{
    public class PersonService : Service<Person>
    {
        public PersonService(PersonRepository repository): base(repository)
        {            
        }
    }
}
