using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Controller.Model;
using SchoolManagement.Model;
using SchoolManagement.ServiceLayer.Interface;

namespace SchoolManagement.Controller
{
    [Route("api/")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly StudentContext _studentContext;
        readonly IStudent _student;
        public StudentController(StudentContext studentContext, IStudent student
            )
        {
            _studentContext = studentContext;
            _student = student;
        }


        
        [HttpGet("AllStudent")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult ListOfStudent()
        {
            var result = _student.GetAllStudent();
            return Ok(result);
        }

        [HttpGet("Student/{StudentID}")]
        public IActionResult GetStudentByID(int StudentID)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (StudentID != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var result = _studentContext.Student.Where(t => t.StudentID == StudentID).FirstOrDefault();
            return Ok(result);
        }


        [HttpPost("AddStudent")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult AddStudentData([FromBody]Students studentResponse)
        {
            var studentInsert = _studentContext.Student.Add(studentResponse);

            _studentContext.SaveChanges();

            return Ok("inserted");
        }

        [HttpPut("UpdateStudent/{StudentID}")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult UpdateStudentData(int StudentID, [FromBody]Students studentRequest)
        {
            var studentInsert = _studentContext.Student.Where(t => t.StudentID == StudentID).FirstOrDefault();

            studentInsert.FathersName = studentRequest.FathersName;
            studentInsert.MothersName = studentRequest.MothersName;
            studentInsert.MobileNumber = studentRequest.MobileNumber;
            studentInsert.Name = studentRequest.Name;
            studentInsert.Address = studentRequest.Address;

            _studentContext.SaveChanges();

            return Ok("updated");
        }
    }
}