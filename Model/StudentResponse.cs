using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Model
{
    public class StudentResponse
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }
}
