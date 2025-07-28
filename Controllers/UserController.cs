using Microsoft.AspNetCore.Mvc;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Business.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Sport_Club_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UsersController : ControllerBase
    {
        private readonly UserInterface _userService;

        public UsersController(UserInterface userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserDTos>> Create(UserDTos dto)
        {
            var createdUser = await _userService.CreateAsync(dto);
            return Ok(createdUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTos>> GetById(int id)
        {
            var user = await _userService.GetID(id);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTos>> Update(int id, UserDTos dto)
        {
            var updatedUser = await _userService.UpdateAsync(id, dto);
            if (updatedUser == null)
                return NotFound("User not found to update");
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (deleted == null)
                return NotFound("User not found to delete");

            return Ok("User deleted successfully");
        }
    }
}
