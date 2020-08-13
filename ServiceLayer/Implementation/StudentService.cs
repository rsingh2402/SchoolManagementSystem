using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Controller.Model;
using SchoolManagement.Helper;
using SchoolManagement.Model;
using SchoolManagement.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SchoolManagement.ServiceLayer.Implementation
{
    public class StudentService : IStudent
    {
        readonly StudentContext _studentContext;
        private readonly AppSettings _appSettings;
        public StudentService(StudentContext studentContext, IOptions<AppSettings> appSettings)
        {
            _studentContext = studentContext;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Students> GetAllStudent()
        {
            List<StudentResponse> studentResponses = new List<StudentResponse>();

            var studentList = _studentContext.Student.FromSqlRaw("Execute dbo.GetAllStudent");

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Students, StudentResponse>();
            //});

            //IMapper iMapper = config.CreateMapper();
            //var source = new Students();
            //var destination = iMapper.Map<List<Students>, List<StudentResponse>>(studentList);

            return studentList.ToList();
        }
    }
}
