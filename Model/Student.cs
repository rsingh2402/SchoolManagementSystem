using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Controller.Model
{
    public class Students : BaseClass
    {       
        public string Name { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }
}
