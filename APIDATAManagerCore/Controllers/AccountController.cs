using APIDataManager.Library.DataAccess;
using APIDataManager.Library.Models;
using APIDATAManagerCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using APIDATAManagerCore.Code;

namespace APIDATAManagerCore.Controllers
{

    [ApiController]
    [Authorize]
    [Route("Auth")]
    [EnableCors]
    public class AccountController : Controller
    {
        private IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            IActionResult response = Unauthorized();
            //login.password = SecurePasswordHasher.Hash(login.password, 5);
            UserData data = new UserData(_config);
            bool verify = data.Verify(login);
            
            if (verify)
            {
                UserModel user = data.GetUserById(login.username);
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [AllowAnonymous]
        [Route("CreateUser")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUser login)
        {
            IActionResult response = Unauthorized();
            //verify user exists/create user and return user object
            login.Password = SecurePasswordHasher.Hash(login.Password, 5);
            UserData data = new UserData(_config);
            bool done = data.CreateUser(login);
            UserModel user = new UserModel();

            if (done)
            {
                response = Ok();
            }

            return response;
        }

        /*[HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }
            return new string[] { "value1", "value2", "value3", "value4", "value5" };
        }*/

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("DateOfJoing", userInfo.CreateTime.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
