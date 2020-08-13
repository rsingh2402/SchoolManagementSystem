using Microsoft.EntityFrameworkCore;
using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Controller.Model
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }

        public DbSet<Students> Student { get; set; }
        public DbSet<Signup> Signup { get; set; }
    }
}
