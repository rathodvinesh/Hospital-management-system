using HMAS.DTO.Doctors;
using HMAS.DTO.Leaves;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMAS.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("doctor")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO doctorDTO)
        {
            var doc = await _service.AddDoctor(doctorDTO);
            return Ok(new { status = "success", message = "Doctor added", data = doc });
        }

        //[Authorize(Roles = "Doctor")]
        [HttpPost("doctor/leave")]
        public async Task<IActionResult> AddLeave([FromBody] LeaveDTO leaveDTO)
        {
            var leave = await _service.ApplyLeave(leaveDTO);
            return Ok(new { status = "success", message = "Leave applied", data = leave });
        }

        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("doctors")]
        public async Task<IActionResult> GetAll()
        {
            var docs = await _service.GetAll();
            if (docs == null || !docs.Any())
            {
                return NotFound(new { status = "error", message = "No doctors found", data = new object() });
            }
            return Ok(new { status = "success", message = "Doctors retrieved", data = docs });
        }

        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doc = await _service.GetById(id);
            if (doc == null)
            {
                return NotFound(new { status = "error", message = "Doctor not found", data = new object() });
            }
            return Ok(new { status = "success", message = "Doctor found", data = doc });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("doctor/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorDTO dto)
        {
            var updated = await _service.UpdateDoctor(id, dto);
            if (updated == null)
            {
                return NotFound(new { status = "error", message = "Doctor not found", data = new object() });
            }
            return Ok(new { status = "success", message = "Doctor updated", data = updated });
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("doctor/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteDoctor(id);
            if (!deleted)
            {
                return NotFound(new { status = "error", message = "Doctor not found", data = new object() });
            }
            return Ok(new { status = "success", message = "Doctor deleted", data = new object() });
        }

        //[Authorize(Roles = "Doctor")]
        [HttpPost("doctor/{id}/availability")]
        public async Task<IActionResult> SetAvailability(int id, [FromBody] List<DoctorAvailDTO> dtos)
        {
            if (dtos == null || !dtos.Any())
                return BadRequest(new { status = "error", message = "Availability data is required", data = new object() });

            await _service.AddAvailabilityAsync(id, dtos);
            return Ok(new { status = "success", message = "Availability updated", data = new object() });
        }

        //[Authorize(Roles = "Doctor,Receptionist")]
        [HttpGet("doctor/{id}/availability")]
        public async Task<IActionResult> GetAvailability(int id)
        {
            var avail = await _service.GetAvailabilityAsync(id);

            if (avail == null || !avail.Any())
            {
                return NotFound(new { status = "error", message = "No availability found", data = new object() });
            }

            return Ok(new { status = "success", message = "Availability fetched", data = avail });
        }
    }
}



//using HMAS.DTO.Doctors;
//using HMAS.DTO.Leaves;
//using HMAS.Models;
//using HMAS.Services;
//using HMAS.Services.Interface;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

//namespace HMAS.Controllers
//{
//    [Route("api/v1")]
//    [ApiController]
//    public class DoctorController : ControllerBase
//    {
//        private readonly IDoctorService _service;

//        public DoctorController(IDoctorService service)
//        {
//            _service = service;
//        }

//        //[Authorize(Roles = "Admin")]
//        [HttpPost("doctor")]
//        public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO doctorDTO)
//        {
//            var doc = await _service.AddDoctor(doctorDTO);
//            return Ok(new { status = "success", data = doc });
//        }

//        //[Authorize(Roles = "Doctor")]
//        [HttpPost("doctor/leave")]
//        public async Task<IActionResult> AddLeave([FromBody] LeaveDTO leaveDTO)
//        {
//            var leave = await _service.ApplyLeave(leaveDTO);
//            return Ok(new { status = "success", data = leave });
//        }

//        //[Authorize(Roles = "Admin,Receptionist")]
//        [HttpGet("doctors")]
//        public async Task<IActionResult> GetAll()
//        {
//            var docs = await _service.GetAll();
//            if (docs == null)
//            {
//                return NotFound(new {status="Success",message="No data found for doctors"});
//            }
//            return Ok(new { status = "success", data = docs });
//        }

//        //[Authorize(Roles = "Admin,Receptionist")]
//        [HttpGet("doctor/{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var doc = await _service.GetById(id);
//            if (doc == null)
//            {
//                return NotFound(new { status = "Success", message = "No data found for doctors" });
//            }
//            return Ok(new { status = "success", data = doc });
//        }

//        //[Authorize(Roles = "Admin")]
//        [HttpPut("doctor/{id}")]
//        public async Task<IActionResult> Update(int id, [FromBody] DoctorDTO dto)
//        {
//            var updated = await _service.UpdateDoctor(id, dto);
//            return updated == null ? NotFound(new { status = "Success", message = "No data found for doctors" }) : Ok(new { status = "success", data = updated });
//        }

//        //[Authorize(Roles = "Admin")]
//        [HttpDelete("doctor/{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var deleted = await _service.DeleteDoctor(id);
//            return deleted ? NoContent() : NotFound(new { status = "Success", message = "No data found for doctors" });
//        }

//        //[Authorize(Roles = "Doctor")]
//        [HttpPost("doctor/{id}/availability")]
//        public async Task<IActionResult> SetAvailability(int id, [FromBody] List<DoctorAvailDTO> dtos)
//        {
//            if (dtos == null || !dtos.Any())
//                return BadRequest("Availability data is required.");

//            await _service.AddAvailabilityAsync(id, dtos);
//            return Ok("Availability updated successfully.");
//        }

//        //[Authorize(Roles = "Doctor,Receptionist")]
//        [HttpGet("doctor/{id}/availability")]
//        public async Task<IActionResult> GetAvailability(int id)
//        {
//            var avail = await _service.GetAvailabilityAsync(id);

//            return avail == null ? NotFound(new { status = "problem", message = "No data found for doctors" }) : Ok(new { status = "success", data = avail });
//        }
//    }
//}
