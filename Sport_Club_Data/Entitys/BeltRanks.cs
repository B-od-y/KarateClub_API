using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Data.Entitys
{
    public class BeltRanks
    {
        public int BeltRankID { get; set; }
        public string BeltName { get; set; } 
        public decimal BeltTestFees { get; set; }
        public ICollection<Members>? members{ get; set; } = new List<Members>(); // Navigation property (M to 1 relationship)
        public ICollection<BeltTests>? Belttests { get; set; } = new List<BeltTests>();
    }

}
