using AulasWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Services
{
    public class BaseService<T> : IService<T> where T : BaseModel
    {
        public static List<T> list { get; set; } = new List<T>();

        public void Create(T model)
        {
            list.Add(model);
        }

        public void Delete(int id)
        {
            T item = this.ReadById(id);
            list.Remove(item);
        }

        public List<T> Read()
        {
            return list;
        }

        public T ReadById(int id)
        {
            T item = list.FirstOrDefault(i => i.Id == id);
            return item;
        }

        public void Update(T model)
        {
            T oldItem = this.ReadById(model.Id);
            this.Delete(oldItem.Id);
            this.Create(model);
        }
    }
}
