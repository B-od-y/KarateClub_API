using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Bussiness.DTOs
{
    public class MemberInstructorDTos
    {
        public MemberDTos member { get; set;}

        public instructorDTos instructor { get; set;}

        public DateTime AssignDate { get; set;} = DateTime.Now;
    }
}
