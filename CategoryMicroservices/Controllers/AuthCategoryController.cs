using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MS.API.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CategoryMicroservices.Controllers
{
    [Route("api/authcategory")]
    [ApiController]
    public class AuthCategoryController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<AuthCategoryController> _logger;

        public AuthCategoryController(IConfiguration config, ILogger<AuthCategoryController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginModel data)
        {
            IActionResult response = Unauthorized();
            try
            {
                var user = AuthenticateUser(data);
                if (user.UserName != null && user.Password != null)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    response = Ok(new { Token = $"Bearer  {tokenString}", Message = "Operation Successful" });
                }
                //else
                //{ response = Ok(new { Token = "", Message = "In-correct clientid or password" }); }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                response = Ok(new { Token = "", Message = "Error Occurred" });//Unauthorized();
            }

            return response;
        }

        private string GenerateJSONWebToken(LoginModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(5),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private LoginModel AuthenticateUser(LoginModel login)
        {
            LoginModel user = new LoginModel();
            string pwd = _config["Credentials:PassWord"];
            string? _pwd = login.Password;
            string _username = _config["Credentials:UserName"];
            //int _clientId = Convert.ToInt32(ClientID);


            if (login.UserName == _username && _pwd == pwd)
            {
                user = new LoginModel { UserName = _username, Password = _pwd };
            }
            return user;
        }
    }
}
