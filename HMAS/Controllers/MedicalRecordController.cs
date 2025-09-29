using HMAS.DTO.Medical_Record;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMAS.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalService _medicalService;

        public MedicalRecordController(IMedicalService medicalService)
        {
            _medicalService = medicalService;
        }

        //[Authorize(Roles = "Doctor")]
        [HttpPost("medical/record")]
        public async Task<IActionResult> AddRecord([FromBody] MedicalDTO dto)
        {
            await _medicalService.AddRecord(dto);
            return Ok(new
            {
                status = "success",
                message = "Medical record added successfully.",
                data = dto
            });
        }

        //[Authorize(Roles = "Doctor,Admin")]
        [HttpGet("medical/records")]
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _medicalService.GetAllRecords();
            if (records == null || !records.Any())
            {
                return NotFound(new
                {
                    status = "error",
                    message = "No medical records found.",
                    data = new object()
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Medical records retrieved successfully.",
                data = records
            });
        }

        //[Authorize(Roles = "Doctor,Admin")]
        [HttpGet("medical/record/{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var record = await _medicalService.GetRecord(id);
            if (record == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "Medical record not found.",
                    data = new object()
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Medical record retrieved successfully.",
                data = record
            });
        }
    }
}
