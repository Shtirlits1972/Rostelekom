using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public bool Availability { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, EquipmentId = {EquipmentId}, EquipmentName = {EquipmentName}, Quantity = {Quantity}, Location = {Location}, Availability = {Availability}";
        }
    }
}
