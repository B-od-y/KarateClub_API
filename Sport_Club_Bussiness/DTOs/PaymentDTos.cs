using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness.DTOs
{
    public class PaymentDTos
    {
        public int PaymentID { get; set; }
        public MemberDTos? Member { get; set; } // Navigation property (Many to 1 relationship)
        public DateTime? Data { get; set; }
        public decimal Amount { get; set; }
    }
}
