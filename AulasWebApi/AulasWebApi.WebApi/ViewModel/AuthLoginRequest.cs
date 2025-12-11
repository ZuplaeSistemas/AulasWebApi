using System.ComponentModel.DataAnnotations;

namespace AulasWebApi.WebApi.ViewModel
{
    public class AuthLoginRequest
    {
        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo E-mail está em um formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo Senha deve ter no mínimo 3 caracteres")]
        public string Password { get; set; }
    }
}
