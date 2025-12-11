using AulasWebApi.Services;
using AulasWebApi.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AulasWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            this._service = service;
        }

        [HttpPost("Login")]
        public IActionResult Login(AuthLoginRequest request)
        {
            try
            {
                string retorno = this._service.Login(request.Email, request.Password);
                AuthLoginResponse response = new AuthLoginResponse();
                response.Token = retorno;
                response.Message = "Login realizado com sucesso";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        [HttpPost("Logout")]
        public string Logout(int userId)
        {
            string message = this._service.Logout(userId);
            return message;
        }
    }
}
