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
    public class BeltRankController : ControllerBase
    {
        private readonly BeltRankInterface _beltRankService;

        public BeltRankController(BeltRankInterface beltRankService)
        {
            _beltRankService = beltRankService;
        }

        [HttpGet("GetAllBeltRanks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var ranks = await _beltRankService.GetAllAsync();
            if (ranks == null || !ranks.Any())
                return NotFound("No belt ranks found.");

            return Ok(ranks);
        }

        [HttpGet("GetBeltRankById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var rank = await _beltRankService.GetByIdAsync(id);
            if (rank == null)
                return NotFound($"Belt rank with ID {id} not found.");

            return Ok(rank);
        }

        [HttpPost("AddBeltRank")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] BeltRankDTos beltDto)
        {
            if (beltDto == null)
                return BadRequest("Belt rank data is required.");

            var created = await _beltRankService.AddAsync(beltDto);
            return CreatedAtAction(nameof(GetById), new { id = created.BeltRankID }, created);
        }

        [HttpPut("UpdateBeltRank/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] BeltRankDTos beltDto)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var updated = await _beltRankService.UpdateAsync(id, beltDto);
            if (updated == null)
                return NotFound($"Belt rank with ID {id} not found.");

            return Ok(updated);
        }

        [HttpDelete("DeleteBeltRank/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var deleted = await _beltRankService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Belt rank with ID {id} not found.");

            return NoContent();
        }
    }
}
