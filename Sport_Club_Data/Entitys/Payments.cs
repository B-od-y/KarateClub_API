using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Data.Entitys
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public int MemberID { get; set; } // Foreign Key
        public Members? Member { get; set; } // Navigation property (Many to 1 relationship)
        public DateTime? Data { get; set; }
        public decimal Amount { get; set; }
        public BeltTests? BeltTest { get; set; }
        public SubscriptionPeriods? SubscriptionPeriod { get; set; }
    }
}
