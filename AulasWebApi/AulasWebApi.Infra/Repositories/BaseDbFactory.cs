using AulasWebApi.Infra.Db;
using AulasWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Infra.Repositories
{
    public class BaseDbFactory<T> : IRepository<T> where T : BaseModel
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private readonly string tablename = typeof(T).Name.ToLower();
        public BaseDbFactory(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        public int Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> Read()
        {
            throw new NotImplementedException();
        }

        public T ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
