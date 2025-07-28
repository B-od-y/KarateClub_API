using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;
using Sport_Club_Business;
using Sport_Club_Bussiness;
using System;
using Sport_Club_Business.Services;
using Sport_Club_Business.Interface;
using Microsoft.AspNetCore.Authorization;
namespace Sport_Club_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorInterface _instructorService;

        public InstructorController(InstructorInterface InstructorService)
        {
            _instructorService = InstructorService;
        }
       
        [HttpGet("GetAllInstructors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<instructorDTos>>> GetInstructors()
        {
            var instructors = await _instructorService.GetAllAsync();
            return Ok(instructors);
        }

        [HttpGet("GetInstructorBy{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<instructorDTos>>> GetInstructorByID(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }

            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound($"Instructor with ID {id} not found.");
            }
            return Ok(instructor);
        }

        [HttpPost("AddInstructor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddInstructor([FromBody] instructorDTos instructorDto)
        {
            var result = await _instructorService.AddAsync(instructorDto);
            if (result == null)
            {
                return BadRequest("Failed to add instructor. Please check the provided data.");
            }
            return CreatedAtAction(nameof(GetInstructorByID), new { id = result.InstructorID }, result);
        }

        [HttpPut("UpdateInstructor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateInstructor(int id, [FromBody] instructorDTos instructorDto)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID or data provided.");
            }
            var updated = await _instructorService.UpdateAsync(id, instructorDto);
            if (updated == null)
            {
                return NotFound($"Instructor with ID {id} not found.");
            }
            return CreatedAtAction(nameof(GetInstructorByID), new { id = updated.InstructorID }, updated);
        }

        [HttpDelete("DeleteInstructor/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteInstructor(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            var deleted = await _instructorService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Instructor with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
