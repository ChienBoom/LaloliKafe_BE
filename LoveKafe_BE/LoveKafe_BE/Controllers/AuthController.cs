using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoveKafe_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        public AuthController(
             UserManager<AppUser> userManager,
             RoleManager<IdentityRole> roleManager,
             IConfiguration configuration,
             AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var refreshToken = GetRefreshToken(authClaims);
                var accessToken = GetAccessToken(authClaims);
                var userDetail = _appDbContext.UserDetail.Where(o => o.Username.Equals(model.Username)).FirstOrDefault();

                return Ok(new
                {
                    refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken),
                    accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    userDetail = userDetail,
                    expirationRefreshToken = refreshToken.ValidTo,
                    expirationAccessToken = accessToken.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("getAccessToken")]
        public async Task<IActionResult> GetAccessToken([FromBody] Token value)
        {
            var refreshToken = value.RefreshToken;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(refreshToken) as JwtSecurityToken;
            if (jsonToken != null)
            {
                var userName = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

                if (userName != null)
                {
                    var user = await _userManager.FindByNameAsync(userName);

                    if (user != null)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }
                        var accessToken = GetAccessToken(authClaims);

                        return Ok(new
                        {
                            accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                            expirationAccessToken = accessToken.ValidTo
                        });
                    }
                    return Unauthorized();
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            UserDetail userDetail = new()
            {
                Username = model.Username,
                Email = model.Email,
                FullName = model.Fullname,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                Sex = model.Sex,
                UrlImage = model.UrlImage
            };
            _appDbContext.UserDetail.Add(userDetail);
            _appDbContext.SaveChanges();

            //lay id nguoi dung vua tao
            var uD = _appDbContext.UserDetail.FirstOrDefault(o => o.Username.Equals(model.Username));


            AppUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                UserDetailId = uD.Id
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRole.User);
            }
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [Authorize]
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            //tao chi tiet nguoi dung UserDetail
            UserDetail userDetail = new()
            {
                Username = model.Username,
                Email = model.Email,
                FullName = model.Fullname,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                Sex = model.Sex,
                UrlImage = model.UrlImage
            };
            _appDbContext.UserDetail.Add(userDetail);
            _appDbContext.SaveChanges();

            //lay id nguoi dung vua tao
            var uD = _appDbContext.UserDetail.FirstOrDefault(o => o.Username.Equals(model.Username));


            AppUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                UserDetailId = uD.Id
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            //if (!await _roleManager.RoleExistsAsync(UserRole.User))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRole.User));

            if (await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRole.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [Authorize]
        [HttpGet("my-profile")]
        public async Task<IActionResult> getProfile()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _appDbContext.UserDetail.FirstOrDefault(user => user.Username.Equals(userName));
            if (user == null) return NotFound();
            return Ok(user);
        }

        private JwtSecurityToken GetRefreshToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMonths(6),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private JwtSecurityToken GetAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                // expires: DateTime.Now.AddDays(5),
                expires: DateTime.Now.AddMinutes(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
