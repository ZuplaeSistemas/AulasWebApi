namespace AulasWebApi.WebApi.ViewModel
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
