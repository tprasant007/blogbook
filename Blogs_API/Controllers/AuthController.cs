using Blogs_API.DTO;
using Blogs_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogs_API.Controllers
{
    [Route("api/usersAuth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _authRepo;
        public AuthController(IUserRepository userRepo)
        {
            _authRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response =  await _authRepo.Login(loginDto);
            if (response == null || string.IsNullOrEmpty(response.Token))
            {
                return Unauthorized("Invalid Credentials");
            }  
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            bool ifUserNameUnique = await _authRepo.IsUniqueUserAsync(request.UserName, request.Email);
            if (!ifUserNameUnique)
            {
                return BadRequest("Username or Email already exists.");
            }
            var user = await _authRepo.Register(request);
            if (user == null) return BadRequest("Something went wrong");
            return Ok(user.UserName);

        }
    }
}
