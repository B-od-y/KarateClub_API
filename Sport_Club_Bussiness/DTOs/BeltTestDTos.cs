using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Bussiness.DTOs
{
    public class BeltTestDTos
    {
        public int TestID { get; set; }
        public MemberDTos Member { get; set; }
        public BeltRankDTos BeltRank { get; set; }
        public bool Result { get; set; }
        public instructorDTos TestedByInstructor { get; set; }
        public PaymentDTos Payment { get; set; }
    }
}
