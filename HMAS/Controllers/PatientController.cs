using HMAS.DTO.Patient;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMAS.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("patient")]
        public async Task<IActionResult> AddPatient([FromBody] PatientDTO patientDTO)
        {
            var pat = await _service.AddPatient(patientDTO);

            if (pat == null)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Failed to add patient.",
                    data = new object()
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Patient added successfully.",
                data = pat
            });
        }

        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("patients")]
        public async Task<IActionResult> GetAll()
        {
            var allPat = await _service.GetAll();

            if (allPat == null || !allPat.Any())
            {
                return NotFound(new
                {
                    status = "error",
                    message = "No patients found.",
                    data = new object()
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Patients retrieved successfully.",
                data = allPat
            });
        }

        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("patient/search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var pat = await _service.Search(keyword);

            if (pat == null || !pat.Any())
            {
                return NotFound(new
                {
                    status = "error",
                    message = $"No patients found matching '{keyword}'.",
                    data = new object()
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Search results retrieved.",
                data = pat
            });
        }

        //[Authorize(Roles = "Admin,Receptionist,Doctor,Patient")]
        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pat = await _service.GetById(id);

            if (pat == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "Patient not found.",
                    data = new object()
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Patient details retrieved.",
                data = pat
            });
        }
    }
}
