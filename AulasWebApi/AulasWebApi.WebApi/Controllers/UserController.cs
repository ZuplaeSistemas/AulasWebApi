using AulasWebApi.Models;
using AulasWebApi.Services;
using AulasWebApi.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AulasWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly PersonService _personService;
        public UserController(UserService service, PersonService personService)
        {
            _service = service;
            _service.Read();
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<User> model = this._service.Read();            

            List<UserGetResponse> response = new List<UserGetResponse>();
            foreach(var u in model)
            {
                UserGetResponse userResponse = new UserGetResponse();
                userResponse.Id = u.Id;
                userResponse.Email = u.Email;
                userResponse.CreatedAt = u.CreatedAt;
                userResponse.Person = this._personService.ReadById(u.Person_Id);
                response.Add(userResponse);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User model = this._service.ReadById(id);
            UserGetResponse response = new UserGetResponse
            {
                Id = model.Id,
                Email = model.Email,
                CreatedAt = model.CreatedAt,
                Person = this._personService.ReadById(model.Person_Id)
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
        public IActionResult Post([FromBody] UserPostRequest viewModel)
        {
            User model = new User
            {
                Email = viewModel.Email,
                Password = viewModel.Password,
                Person_Id = viewModel.Person_Id
            };
            this._service.Create(model);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserPutRequest request)
        {
            User model = new User();
            model.Id = id;
            model.Password = request.Password;

            this._service.Update(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this._service.Delete(id);                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
