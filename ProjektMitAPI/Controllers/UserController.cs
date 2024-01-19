using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektMitAPI.Data;
using ProjektMitAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;

namespace ProjektMitAPI.Controllers
{
    public class LoginInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterInfo
    {
        public string Firstname { get; set; }
        public string Familyname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserToken
    {
        public string Email { get; set; }
        public string JWT { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MovieAppContext _context; 
        public UsersController(MovieAppContext context) { _context = context; }


        [HttpPost("register")]
        public IActionResult Register(RegisterInfo login)
        {
            User userInDb = _context.Users.FirstOrDefault(user => user.email == login.Email);

            if (userInDb == null)
            {
                string salt;
                string pwHash = HashGenerator.GenerateHash(login.Password, out salt);
                User newUser = new User()
                {
                    firstName = login.Firstname,
                    familyName = login.Familyname,
                    username = login.Username,
                    email = login.Email,
                    password = pwHash,
                    Salt = salt,
                };
                _context.Users.Add(newUser);

                _context.SaveChanges();

                return Ok(CreateToken(newUser.Id, newUser.email));
            }

            return BadRequest();
        }


        [HttpPost("login")]
        public IActionResult Login(LoginInfo login)
        {
            User userInDb = _context.Users.FirstOrDefault(user => user.email == login.Email);

            if (userInDb != null
                && HashGenerator.VerifyHash(userInDb.password, login.Password, userInDb.Salt))
            {
                return Ok(CreateToken(userInDb.Id, userInDb.email));
            }
            return Unauthorized();
        }


        [Authorize]
        [HttpGet("getCurrentUser")]
        public IActionResult getCurrentUser()
        {
            try
            {
                var userId = GetUserIdFromToken();
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }


        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteUser()
        {
            var userId = GetUserIdFromToken();

            if (userId != null)
            {
                var user = _context.Users.Include(u => u.likes).FirstOrDefault(u => u.Id == userId);

                if (user != null && user.Id != 1)
                {
                    // Lösche alle MovieUserLiked-Objekte in der Liste likes des Benutzers
                    _context.MoviesUserLiked.RemoveRange(user.likes);

                    // Lösche den Benutzer
                    _context.Users.Remove(user);

                    _context.SaveChanges();
                    return Ok("Benutzer erfolgreich gelöscht");
                }

                return NotFound("Benutzer nicht gefunden");
            }

            return Unauthorized("Ungültiges Token");
        }

        [Authorize]
        [HttpPut("changeCurrentUser")]
        public IActionResult changeCurrentUser(RegisterInfo newUserInfo)
        {

            var userId = GetUserIdFromToken();

            if (userId != null)
            {
                var user = _context.Users.Include(u => u.likes).FirstOrDefault(u => u.Id == userId);

                if (user != null && user.Id != 1)
                {
                    if (!string.IsNullOrEmpty(newUserInfo.Firstname)){ user.firstName = newUserInfo.Firstname; }
                    if (!string.IsNullOrEmpty(newUserInfo.Familyname)) { user.familyName = newUserInfo.Familyname; }
                    if (!string.IsNullOrEmpty(newUserInfo.Username)) { user.username = newUserInfo.Username; }
                    if (!string.IsNullOrEmpty(newUserInfo.Email)) { user.email = newUserInfo.Email; }

                    _context.SaveChanges();


                    return Ok(newUserInfo);
                }

                return NotFound("Benutzer nicht gefunden");
            }

            return Unauthorized("Ungültiges Token");
        }

        private long? GetUserIdFromToken()
        {
            Claim subClaim = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
            long userId = Convert.ToInt64(subClaim.Value);

            return userId;
        }


        private UserToken CreateToken(long userId, string email)
        {
            var expires = DateTime.UtcNow.AddDays(5); 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(JwtRegisteredClaimNames.Sub, $"{userId}"), 
                    new Claim(JwtRegisteredClaimNames.Email, email), 
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = expires,
                Issuer = JwtConfiguration.ValidIssuer,
                Audience = JwtConfiguration.ValidAudience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(JwtConfiguration.IssuerSigningKey)), 
                        SecurityAlgorithms.HmacSha512Signature)
            }; 
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var token = tokenHandler.CreateToken(tokenDescriptor); 
            var jwtToken = tokenHandler.WriteToken(token); 
            return new UserToken { Email = email, ExpiresAt = expires, JWT = jwtToken };
        }
    }
}
