using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class Suppliers
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Terms { get; set; }

        public override string ToString()
        {
            return SupplierName;
        }
    }
}





