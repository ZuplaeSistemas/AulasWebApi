using AulasWebApi.Models;
using AulasWebApi.Services;
using AulasWebApi.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AulasWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private PersonService _service;

        public PersonController(PersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Person> model = this._service.Read();
            List<PersonGetResponse> response = new List<PersonGetResponse>();

            foreach (var item in model)
            {
                PersonGetResponse personResponse = new PersonGetResponse
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    BirthDate = item.BirthDate,
                    CreatedAt = item.CreatedAt
                };
                response.Add(personResponse);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Person model =this._service.ReadById(id);
            PersonGetResponse response = new PersonGetResponse
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                CreatedAt = model.CreatedAt
            };
            return Ok(response);
        }

        [HttpGet("exist/{id}")]
        public IActionResult Exist(int id)
        {
            ExistResponse response = new ExistResponse
            {
                Id = id,
                Exist = this._service.Exists(id)
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonPostRequest request)
        {
            Person model = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            };
            this._service.Create(model);

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonPostRequest request)
        {
            Person model = new Person
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            };
            this._service.Update(model);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
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
