using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public char Gender { get; set; }
    }
}
