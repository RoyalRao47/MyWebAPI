using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyWebAPI.DataBase;
using MyWebAPI.Models;
using MyWebAPI.Service;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json;

namespace MyWebAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public LoginController(DataContext context, IConfiguration configuration, IUserService userService, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
            _httpClient = httpClient;
        }
        [HttpGet("Login")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]

        public async Task<IActionResult> Index(LoginModel model)
        {
            try
            {
                //var domain = _configuration["Domain"].ToString();
                if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                {
                    // return Redirect(domain + $"/swagger/index.html");
                    return Redirect($"/swagger/index.html");
                }

                var userEntity = _userService.GetUser(model.UserName, model.Password);
                if (userEntity == null)
                {
                    throw new Exception("Invalid username and password!!");
                }
                await CreateAuthentication(userEntity);

                //return Redirect(domain + $"/swagger/index.html");
                return Redirect($"/swagger/index.html");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }


        }

        protected async Task CreateAuthentication(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim(ClaimTypes.Email, user.Email)
    };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                , new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromQuery] LoginModel model)
        {
            var tokenString = string.Empty;
            var username = model.UserName;
            var password = model.Password;
            var user = _userService.GetUser(username, password);
            if (user != null)
            {
                //var domain = _configuration["Auth0:Domain"];
                //var clientId = _configuration["Auth0:ClientId"];
                //var clientSecret = _configuration["Auth0:ClientSecret"];
                //var audience = _configuration["Auth0:Audience"];

                //var tokenRequest = new Dictionary<string, string>
                //    {
                //        { "client_id", clientId },
                //        { "client_secret", clientSecret },
                //        { "audience", audience },
                //        { "grant_type", "client_credentials" }
                //    };

                //var requestContent = new FormUrlEncodedContent(tokenRequest);

                //var response = await _httpClient.PostAsync($"https://{domain}/oauth/token", requestContent);

                //if (response.IsSuccessStatusCode)
                //{
                //    var responseContent = await response.Content.ReadAsStringAsync();
                //    var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                //    return Ok(new { Token = tokenResponse["access_token"] });
                //}
                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
                var issuer = _configuration["Jwt:Issuer"].ToString();
                var audience = _configuration["Jwt:Audience"].ToString();
                var authSigningKey = new SymmetricSecurityKey(key);
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.Now.AddMinutes(60),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
