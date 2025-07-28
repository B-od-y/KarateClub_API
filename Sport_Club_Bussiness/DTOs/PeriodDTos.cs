using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Bussiness.DTOs
{
    public class PeriodDTos
    {
        public int PeriodID {  get; set; }
        public MemberDTos member {  get; set; } 
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = new DateTime().AddDays(30);
        public decimal TestFees { get; set; }
        public PaymentDTos payment { get; set; }

    }
}
