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
    public class PaymentController : ControllerBase
    {
        private readonly PaymentInterface _paymentService;

        public PaymentController(PaymentInterface paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("GetAllPayments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            if (payments == null || !payments.Any())
                return NotFound("No payments found.");

            return Ok(payments);
        }

        [HttpGet("GetAllPaymentsBymemberID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByMeberID(int MemberID)
        {
            var payments = await _paymentService.GetAllByMemberIDAsync(MemberID);
            if (payments == null || !payments.Any())
                return NotFound("No payments found.");

            return Ok(payments);
        }

        [HttpGet("GetPaymentById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null)
                return NotFound($"Payment with ID {id} not found.");

            return Ok(payment);
        }

        [HttpPost("AddPayment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] PaymentDTos paymentDto)
        {
            var created = await _paymentService.AddAsync(paymentDto);
            return CreatedAtAction(nameof(GetById), new { id = created.PaymentID }, created);
        }

        [HttpPut("UpdatePayment/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentDTos paymentDto)
        {
            var updated = await _paymentService.UpdateAsync(id, paymentDto);
            if (updated == null)
                return NotFound($"Payment with ID {id} not found.");

            return CreatedAtAction(nameof(GetById), new {id = updated.PaymentID},updated);
        }

        [HttpDelete("DeletePayment/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Payment with ID {id} not found.");

            return NoContent();
        }
    }
}
