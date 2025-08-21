using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Models
{
    public class Person : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        override public string ToString()
        {
            return $"{base.ToString()} - {this.FirstName} {this.LastName} ({this.BirthDate.ToShortDateString()})";
        }
    }
}
