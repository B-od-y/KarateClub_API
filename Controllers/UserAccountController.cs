using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sport_Club_Business.Services;
using Sport_Club_Bussiness.Services;
using Sport_Club_Bussiness.DTOs;
namespace Sport_Club_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly JwtService _jwtService;
        public UserAccountController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("LOGIN")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }
            var response = await _jwtService.Authenticate(request);
            if (response == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(response);
        }

        [HttpPost("REFRESH TOKEN")]

        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Invalid refresh token request.");
            }
            var response = await _jwtService.ValidateRefreshToken(request.Token);
            if (response == null)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }
            return Ok(response);
        }
    }
}
