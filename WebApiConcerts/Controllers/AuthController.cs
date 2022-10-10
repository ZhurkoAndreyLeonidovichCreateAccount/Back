using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiConcerts.Models;


namespace WebApiConcerts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        IConfiguration _configuration;
        public AuthController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context; 
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                    return BadRequest();

                var email = await _userManager.FindByEmailAsync(model.Email);
                if(email != null)
                {
                    return BadRequest("Пользователь с таким емайл уже существует");
                }
                var applicationUser = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(applicationUser, model.Password);

                if (result.Succeeded)                
                {
                   await _userManager.AddToRoleAsync(applicationUser, "user");
                   return Ok();
                }
               
            }
            return BadRequest("Some data is not valid");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByEmailAsync(userLogin.Email);
                if (currentUser == null)
                {
                   
                    return NotFound("Invalid email");
                   
                }

                var result = await _userManager.CheckPasswordAsync(currentUser, userLogin.Password);
                if (!result)
                {
                    return BadRequest("Invalid password");
                }
                var role = await _userManager.GetRolesAsync(currentUser);
                var claims = new[]
                {
                     new Claim(ClaimTypes.Email,userLogin.Email),
                     new Claim(ClaimTypes.NameIdentifier, currentUser.Id),
                     new Claim(ClaimTypes.Role, role[0])
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenAsString);
            }

            return BadRequest("Some properties are not valid");
        }

         [HttpGet("User")]
         [Authorize]
        public async Task<ActionResult<UserModel>> GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity != null)
            {
                var userClaims = identity.Claims;
                string id = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;


                return new UserModel

                {
                    Id = id,

                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,

                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,

                    Tickets = _context.Tickets.Where(t=>t.UserId == id).ToList(),
                };
            }
            return BadRequest();
        }

    }
}
