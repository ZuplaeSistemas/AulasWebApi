using AulasWebApi.Models;
using AulasWebApi.Services;
using AulasWebApi.WebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AulasWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        public List<UserViewModel> Get()
        {
            List<User> users = this._service.Read();            

            List<UserViewModel> listViewModel = new List<UserViewModel>();
            foreach(var u in users)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.Id = u.Id;
                uvm.Email = u.Email;
                uvm.CreatedAt = u.CreatedAt;     
                uvm.Person = this._personService.ReadById(u.Person_Id);
                listViewModel.Add(uvm);
            }
            return listViewModel;
        }

        [HttpGet("{id}")]
        public UserViewModel Get(int id)
        {
            User user = this._service.ReadById(id);
            UserViewModel uvm = new UserViewModel();
            uvm.Id = user.Id;
            uvm.Email = user.Email;
            uvm.CreatedAt = user.CreatedAt;
            uvm.Person = this._personService.ReadById(user.Person_Id);

            return uvm;
        }

        [HttpGet("exist/{id}")]
        public bool Exist(int id)
        {
            return this._service.Exists(id);
        }

        [HttpPost]
        public void Post([FromBody] User model)
        {
            this._service.Create(model);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User model)
        {
            if (id != model.Id)
            {
                throw new ArgumentException("O ID do objeto User não é igual ao ID da URL.");
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
