using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sport_Club_Data.Entitys
{
    public class BeltTests
    {
        public int TestID { get; set; }
        public int MemberID { get; set; }
        public Members? Member { get; set; }
        public int RankID { get; set; }
        public BeltRanks? BeltRank { get; set; }
        public bool Result { get; set; }
        public int TestedByInstructorID { get; set; }
        public Instructors? Instructor { get; set; }
        public int PaymentID { get; set; }
        public Payments? Payment { get; set; }
    }
}
