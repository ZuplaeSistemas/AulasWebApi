using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Services
{
    public interface IService<T>
    {
        int Create(T model);

        List<T> Read();

        T ReadById(int id);        

        void Update(T model);

        void Delete(int id);
    }
}
