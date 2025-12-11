using System.ComponentModel.DataAnnotations;

namespace AulasWebApi.WebApi.ViewModel
{
    public class UserPutRequest
    {
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo Senha deve ter no mínimo 3 caracteres")]
        public string Password { get; set; }
    }
}
