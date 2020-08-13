using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Controller.Model;
using SchoolManagement.Helper;
using SchoolManagement.Model;

namespace SchoolManagement.Controller
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        readonly StudentContext _studentContext;
        private readonly AppSettings _appSettings;

        public LoginController(IConfiguration configuration, StudentContext studentContext, IOptions<AppSettings> appSettings)
        {
            _config = configuration;
            _studentContext = studentContext;
            _appSettings = appSettings.Value;
        }

        [HttpPost("SignUP")]
        public IActionResult SignUp([FromBody]Signup signup)
        {
            var Emailexist = _studentContext.Signup.Where(t => t.Email == signup.Email).Select(t => t.Email).FirstOrDefault();

            if(Emailexist != null)
            {
                return NotFound("Email Already Exist");
            }

            var result = _studentContext.Signup.Add(signup);

            string email = result.Entity.Email;

            _studentContext.SaveChanges();

              var emailBody = "<h1>a b You Registered Successfull for ABCD School<h1>";

            string emailBody1 = emailBody.Replace("a", result.Entity.FirstName).Replace("b", result.Entity.LastName);

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("r.singh2402@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Registration Confirmation";
                mail.Body = emailBody1;
                mail.IsBodyHtml = true;
                //  mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("r.singh2402@gmail.com", "abesec2012-16");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

            return Ok("SignUp Succesfull");

        }

        [AllowAnonymous]
        [HttpPost("SignIN")]
        public IActionResult Login([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = _studentContext.Signup.Where(t => t.Email == login.Email
            && t.Password == login.password).FirstOrDefault();

            if (user != null)
            {
                UserModel userModel = new UserModel();
                userModel.Email = user.Email;
                userModel.password = user.Password;
                userModel.SignupID = user.SignupID;
                userModel.Role = user.Role;

                var tokenString = GenerateJSONWebToken(userModel);
                response = Ok(new { token = tokenString });
                return response;
            }
            else
                return Ok("Email or password wrong");
        }


        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userInfo.SignupID.ToString()),
                    new Claim(ClaimTypes.Role, userInfo.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}