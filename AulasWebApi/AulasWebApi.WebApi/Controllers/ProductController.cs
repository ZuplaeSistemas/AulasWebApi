using AulasWebApi.Models;
using AulasWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AulasWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Product> Get()
        {
            return this._service.Read();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return this._service.ReadById(id);
        }

        [HttpGet("exist/{id}")]
        public bool Exist(int id)
        {
            return this._service.Exists(id);
        }

        [HttpPost]
        public void Post([FromBody] Product model)
        {
            this._service.Create(model);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product model)
        {
            if (id != model.Id)
            {
                throw new ArgumentException("O ID do objeto Product não é igual ao ID da URL.");
            }
            this._service.Update(model);
        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            try
            {
                this._service.Delete(id);
                StatusCodeResult result = new StatusCodeResult(204);
                return result;
            }
            catch (Exception ex)
            {
                StatusCodeResult result = new StatusCodeResult(500);
                return result;
            }
        }
    }
}
