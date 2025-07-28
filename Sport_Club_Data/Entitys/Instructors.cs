using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Data.Entitys
{
    public class Instructors
    {
        public int ID { get; set; }
        public int PersonID { get; set; } // Foreign Key
        public Persons Person { get; set; } // Navigation property (1 to 1 relationship)
        public string? Qualification { get; set; }
        public ICollection<BeltTests>? BeltTest { get; set; } = new HashSet<BeltTests>();

        public MemberInstructors MemberInstructor { get; set; }
    }
}
