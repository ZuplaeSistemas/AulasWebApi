using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Models
{
    public class User : BaseModel // Herança
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public int Person_Id { get; set; }
    }
}
