using AulasWebApi.Models;

namespace AulasWebApi.WebApi.ViewModel
{
    public class UserGetResponse : BaseViewModel
    {
        public string Email { get; set; }   

        // Foreign Key para Person - POO Composição
        public Person Person { get; set; }
    }
}
