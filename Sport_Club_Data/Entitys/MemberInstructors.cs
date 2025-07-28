using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Data.Entitys
{
    public class MemberInstructors
    {
        public int MemberID {  get; set; }
        public Members Member { get; set; }
        public int InstructorID {  get; set; }
        public Instructors Instructor { get; set; }
        public DateTime AssignDate { get; set; }
    }
}
