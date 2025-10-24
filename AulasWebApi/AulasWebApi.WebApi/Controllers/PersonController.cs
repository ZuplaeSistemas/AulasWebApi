using AulasWebApi.Models;
using AulasWebApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AulasWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private PersonService _service;

        public PersonController(PersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Person> Get()
        {
            return this._service.Read();
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return this._service.ReadById(id);
        }

        [HttpPost]
        public void Post([FromBody] Person model)
        {
            this._service.Create(model);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Person model)
        {
            if (id != model.Id)
            {
                throw new ArgumentException("O ID do objeto Person não é igual ao ID da URL.");
            }
            this._service.Update(model);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this._service.Delete(id);
        }
    }
}
