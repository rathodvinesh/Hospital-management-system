using HMAS.DTO.Appointments;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMAS.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointService _appointService;

        public AppointmentController(IAppointService appointService)
        {
            _appointService = appointService;
        }

        // [Authorize(Roles = "Receptionist")]
        [HttpPost("appointment")]
        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDTO)
        {
            var result = await _appointService.AddAppointment(appointmentDTO);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    status = "Failed",
                    message = result.Message
                });
            }

            return Ok(new
            {
                status = "Success",
                message = result.Message,
                data = result.Appointment
            });
        }

        // [Authorize(Roles = "Receptionist,Admin,Doctor")]
        [HttpGet("appointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appo = await _appointService.GetAppo();

            return Ok(new
            {
                status = "Success",
                message = "Appointments fetched successfully.",
                data = appo
            });
        }

        // [Authorize(Roles = "Receptionist,Admin")]
        [HttpGet("appointments/{keyword}")]
        public async Task<IActionResult> GetAppointmentsByKeyword(string keyword)
        {
            var appo = await _appointService.GetAppoByKey(keyword);

            if (appo == null || !appo.Any())
            {
                return NotFound(new
                {
                    status = "Failed",
                    message = "No appointments found for the given keyword."
                });
            }

            return Ok(new
            {
                status = "Success",
                message = "Filtered appointments fetched successfully.",
                data = appo
            });
        }

        // [Authorize(Roles = "Receptionist")]
        [HttpPut("appointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, AppointmentDTO appointmentDTO)
        {
            var updated = await _appointService.UpdateAppo(id, appointmentDTO);

            if (updated == null)
            {
                return NotFound(new
                {
                    status = "Failed",
                    message = "Appointment not found or could not be updated."
                });
            }

            return Ok(new
            {
                status = "Success",
                message = "Appointment updated successfully.",
                data = updated
            });
        }
    }
}
