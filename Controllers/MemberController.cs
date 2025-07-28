using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sport_Club_Business.Interface;
using Sport_Club_Business.Services;
using Sport_Club_Bussiness;
using Sport_Club_Bussiness.DTOs;

namespace Sport_Club_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        public MemberInterface _MemberService;
        public MemberController(MemberInterface memberInterface)
        {
            _MemberService = memberInterface;
        }

        [HttpGet("GetAllMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMember()
        {
            var members = await _MemberService.GetAllAsync();
            if (members == null || !members.Any())
            {
                return NotFound("No members found.");
            }
            return Ok(members);
        }

        [HttpGet("GetAllMemberWithBeltRank")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMemberWithBeltRank(int RankBeltID)
        {
            var members = await _MemberService.GetAllMemberwithBeltRankAsync(RankBeltID);
            if (members == null || !members.Any())
            {
                return NotFound("No members found.");
            }
            return Ok(members);
        }

        [HttpGet("GetMemberBy{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMemberById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            var member = await _MemberService.GetByIdAsync(id);

            if (member == null)
            {
                return NotFound($"Member with ID {id} not found.");
            }
            return Ok(member);
        }
        [HttpPost("AddMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddInstructor([FromBody] MemberDTos memberD)
        {
            var result = await _MemberService.AddAsync(memberD);
            if (result == null)
            {
                return BadRequest("Failed to add instructor. Please check the provided data.");
            }
            return CreatedAtAction(nameof(GetMemberById), new { id = result.MemberID}, result);
        }

        [HttpPut("UpdateInstructor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateInstructor(int id, [FromBody] MemberDTos memberDtos)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID or data provided.");
            }
            var updated = await _MemberService.UpdateAsync(id, memberDtos);
            if (updated == null)
            {
                return NotFound($"member with ID {id} not found.");
            }
            return CreatedAtAction(nameof(GetMemberById), new { id = updated.MemberID }, updated);
        }

        [HttpDelete("DeleteMember/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteInstructor(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            var deleted = await _MemberService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Member with ID {id} not found.");
            }
            return NoContent();
        }

      
    }
}
