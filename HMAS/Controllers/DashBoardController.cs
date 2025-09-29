using HMAS.DTO.Dashboard;
using HMAS.Services;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMAS.Controllers
{
    [Route("api/v1/dashboard")]
    [ApiController]
    //[Authorize(Roles = "Admin,Receptionist,Doctor")]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashService _dashService;

        public DashBoardController(IDashService service)
        {
            _dashService = service;
        }

        [HttpGet("daily-appointments")]
        public async Task<IActionResult> GetDailyAppointments()
        {
            return Ok(await _dashService.GetDailyAppointmentsByDoctor());
        }

        //[HttpGet("doctor-utilization")]
        //public async Task<IActionResult> GetDoctorUtilization()
        //{
        //    return Ok(await _dashService.GetDoctorUtilization());
        //}

        [HttpGet("patient-frequency")]
        public async Task<IActionResult> GetPatientFrequency()
        {
            return Ok(await _dashService.GetPatientVisitFrequency());
        }
    }
}
