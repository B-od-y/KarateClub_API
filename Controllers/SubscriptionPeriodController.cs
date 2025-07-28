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
    public class SubscriptionPeriodController : ControllerBase
    {
        private readonly SubscriptionPeriodInterface _service;

        public SubscriptionPeriodController(SubscriptionPeriodInterface service)
        {
            _service = service;
        }

        [HttpGet("GetAllPeriods")]
        public async Task<IActionResult> GetAll()
        {
            var periods = await _service.GetAllAsync();
            if (periods == null || !periods.Any())
                return NotFound("No subscription periods found.");

            return Ok(periods);
        }

        [HttpGet("GetPeriodById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var period = await _service.GetByIdAsync(id);
            if (period == null)
                return NotFound($"Subscription period with ID {id} not found.");

            return Ok(period);
        }

        [HttpPost("AddPeriod")]
        public async Task<IActionResult> Add([FromBody] PeriodDTos dto)
        {
            var created = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PeriodID }, created);
        }

        [HttpPut("UpdatePeriod/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PeriodDTos dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"Subscription period with ID {id} not found.");

            return Ok(updated);
        }

        [HttpDelete("DeletePeriod/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Subscription period with ID {id} not found.");

            return NoContent();
        }
    }
}
