using SchoolManagement.Controller.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.ServiceLayer.Interface
{
    public interface IStudent
    {
        IEnumerable<Students> GetAllStudent();
    }
}
