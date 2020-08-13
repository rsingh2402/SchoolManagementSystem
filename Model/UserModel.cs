using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolManagement.Model
{
    public class UserModel
    {
        public string Email { get; set; }
        public string password { get; set; }

        [JsonIgnore]
        public string Role { get; set; }
        [JsonIgnore]
        public int SignupID { get; set; }
    }
}
