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
    public class MemberAssigedInstructorController : ControllerBase
    {
        public MemberInstructorService _MemberInstructorService;
        public MemberAssigedInstructorController(MemberInstructorService MemberInstructorService)
        {
            _MemberInstructorService = MemberInstructorService;
        }

        [HttpGet("GetAllAssignment")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _MemberInstructorService.GetAllAssigment();
            if (result == null)
                return NotFound("Assignments not found.");

            return Ok(result);
        }
        [HttpGet("GetAssignmentBy/memberId")]
        public async Task<IActionResult> GetAssmentByMemberId(int memberId)
        {
            var result = await _MemberInstructorService.GetAllAssigmentByMemberID(memberId);
            if (result == null)
                return NotFound("Assignment not found.");

            return Ok(result);
        }

        [HttpGet("GetAssignmentBy/InstructorID")]
        public async Task<IActionResult> GetAssmentByInstructorId(int InstructorID)
        {
            var result = await _MemberInstructorService.GetAllAssigmentByInstructorID(InstructorID);
            if (result == null)
                return NotFound("Assignment not found.");

            return Ok(result);
        }


        //Assign
        [HttpGet("GetAssignment/{memberId}/{instructorId}")]
        public async Task<IActionResult> GetById(int memberId, int instructorId)
        {
            var result = await _MemberInstructorService.GetAssignByIdAsync(memberId, instructorId);
            if (result == null)
                return NotFound("Assignment not found.");

            return Ok(result);
        }

        [HttpPost("AssignInstructor")]
        public async Task<IActionResult> Add([FromBody] MemberInstructorDTos dto)
        {
            var result = await _MemberInstructorService.AddAsssignAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { memberId = result.member.MemberID, instructorId = result.instructor.InstructorID },
                result);
        }

        [HttpDelete("DeleteAssignment/{memberId}/{instructorId}")]
        public async Task<IActionResult> Delete(int memberId, int instructorId)
        {
            var deleted = await _MemberInstructorService.DeleteAssignAsync(memberId, instructorId);
            if (!deleted)
                return NotFound("Assignment not found.");

            return NoContent();
        }



    }
}
