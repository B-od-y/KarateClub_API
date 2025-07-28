using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sport_Club_Bussiness.DTOs;

namespace Sport_Club_Bussiness
{
    public class instructorDTos
    {
        public int InstructorID { get; set; }
        public PersonDTos person { get; set; }
        public string qualification { get; set; }
    }
}
