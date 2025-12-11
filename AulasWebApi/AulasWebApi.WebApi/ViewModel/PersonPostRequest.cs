using System.ComponentModel.DataAnnotations;

namespace AulasWebApi.WebApi.ViewModel
{
    public class PersonPostRequest
    {

        [Required(ErrorMessage = "O campo FirstName é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo FirstName deve ter no mínimo 3 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo LastName é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo LastName deve ter no mínimo 3 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo BirthDate é obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "O campo BirthDate deve ser uma data válida")]
        public DateTime BirthDate { get; set; }
    }
}
