using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGridC2Consumer.Models
{
    public class Units
    {
        public int Id { get; set; }
        public string UnitName { get; set; } = "Unknown Unit";
        public string Sector { get; set; } = "General";
        public ICollection<Assets> assets { get; set; } = new List<Assets>();
    }
}
