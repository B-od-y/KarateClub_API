using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sport_Club_Business.Interface;
using Sport_Club_Bussiness.DTOs;

namespace Sport_Club_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BeltTestController : ControllerBase
    {
        private readonly BeltTestInterface _beltTestService;

        public BeltTestController(BeltTestInterface beltTestService)
        {
            _beltTestService = beltTestService;
        }

        [HttpGet("GetAllBeltTests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var beltTests = await _beltTestService.GetAllAsync();
            if (beltTests == null || !beltTests.Any())
                return NotFound("No belt tests found.");

            return Ok(beltTests);
        }

        [HttpGet("GetBeltTestById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var beltTest = await _beltTestService.GetByIdAsync(id);
            if (beltTest == null)
                return NotFound($"Belt test with ID {id} not found.");

            return Ok(beltTest);
        }

        [HttpPost("AddBeltTest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] BeltTestDTos dto)
        {
            var created = await _beltTestService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TestID }, created);
        }

        [HttpPut("UpdateBeltTest/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] BeltTestDTos dto)
        {
            var updated = await _beltTestService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"Belt test with ID {id} not found.");

            return Ok(updated);
        }

        [HttpDelete("DeleteBeltTest/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _beltTestService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Belt test with ID {id} not found.");

            return NoContent();
        }
    }
}
