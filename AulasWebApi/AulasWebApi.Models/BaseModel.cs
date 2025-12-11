

namespace AulasWebApi.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{this.Id} - {this.CreatedAt}";
        }
    }
}
