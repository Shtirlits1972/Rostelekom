using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string EmployerName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string Telephone { get; set; }
        public string RoleEmployer { get; set; }

        public override string ToString()
        {
            return EmployerName;
        }
    }
}


