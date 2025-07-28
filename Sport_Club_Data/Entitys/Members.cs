using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Data.Entitys
{
    public class Members
    {
        public int MemberID { get; set; }
        public int PersonID { get; set; } // Foreign Key
        public Persons Person { get; set; } // Navigation property (1 to 1 relationship)
        public string EmergencyContactInfo { get; set; }
        public int? LastBeltRankID { get; set; } // Foreign Key
        public BeltRanks? LastBeltRank { get; set; } // Navigation property (1 to 1 relationship)
        public DateTime? Date { get; set; }
        public bool IsActive { get; set; }
        public ICollection<BeltTests>? BeltTests { get; set; } = new HashSet<BeltTests>();
        public SubscriptionPeriods? SubscriptionPeriods { get; set; }
        public ICollection<Payments>? paymentDto { get; set; } // Navigation property (1 to 1 relationship)

        public ICollection<MemberInstructors> MemberInstructors { get; set; }
    }
}
