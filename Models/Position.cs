using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class Position
    {
        public int Id {  get; set; }
        public string PositionName { get; set; }

        public override string ToString()
        {
            return PositionName;
        }
    }
}
