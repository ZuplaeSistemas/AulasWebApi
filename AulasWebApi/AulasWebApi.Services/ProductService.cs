using AulasWebApi.Infra.Repositories;
using AulasWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Services
{
    public class ProductService : Service<Product>
    {
        public ProductService(ProductRepository repository): base(repository)
        {
            
        }
    }
}
