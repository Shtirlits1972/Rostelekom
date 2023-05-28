using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentModel { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public int EquipmentTypeId { get; set; }
        public string EquipmentTypeName { get; set; }

        public override string ToString()
        {
            return EquipmentName;
        }
    }
}
