using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class EquipmentTypes
    {
        public int Id { get; set; }
        public string EquipmentTypeName { get; set; }

        public override string ToString()
        {
            return EquipmentTypeName;
        }
    }
}
