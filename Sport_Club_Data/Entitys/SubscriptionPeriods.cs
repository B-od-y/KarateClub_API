using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Data.Entitys
{
    public class SubscriptionPeriods
    {
        public int PeriodID { get; set; }
        public DateTime StartData { get; set; }
        public DateTime EndData { get; set; }
        public decimal TestFees { get; set; }
        public int MemberID {  get; set; }
        public Members Member { get; set; }
        public int PaymentID { get; set; }
        public Payments payment { get; set; }

    }
}
