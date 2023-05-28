using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class StatusRequest
    {
        public int Id { get; set; }
        public string StatusRequestName { get; set; }

        public override string ToString()
        {
            return StatusRequestName;
        }
    }
}
