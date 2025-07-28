using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport_Club_Data.Entitys
{
    public class Persons
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? ContactInfo{ get; set; }
        public  string? Address { get; set; }

        public Instructors? Instructor { get; set; } // Navigation property (1 to 1 relationship)
        public Members? Member { get; set; } // Navigation property (1 to 1 relationship)
    }
}
