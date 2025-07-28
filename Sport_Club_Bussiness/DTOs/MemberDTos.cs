using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness.DTOs
{
    public class MemberDTos
    {
        public int MemberID { get; set; }
        public PersonDTos Person { get; set; } 
        public string EmergencyContactInfo { get; set; } 
        public BeltRankDTos? LastBeltRank { get; set; } 
        public DateTime? Date { get; set; }
        public bool IsActive { get; set; }
    }
}
