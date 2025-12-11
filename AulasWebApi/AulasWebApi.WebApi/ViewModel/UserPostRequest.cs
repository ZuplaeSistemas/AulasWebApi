using System.ComponentModel.DataAnnotations;

namespace AulasWebApi.WebApi.ViewModel
{
    public class UserPostRequest
    {
        [Required(ErrorMessage = "O e-mail precisa ser preenchido")]
        [EmailAddress(ErrorMessage = "O campo E-mail está em um formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo Senha deve ter no mínimo 3 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O id da Pessoa precisa ser preenchido")]
        [Range(1, int.MaxValue, ErrorMessage = "O id da Pessoa precisa ser maior que zero")]
        public int Person_Id { get; set; }
    }
}
