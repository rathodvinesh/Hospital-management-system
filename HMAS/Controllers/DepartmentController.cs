using HMAS.DTO.Departments;
using HMAS.Services;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMAS.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDeptService _service;

        public DepartmentController(IDeptService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("department")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDTO dto)
        {
            var dept = await _service.AddDept(dto);
            return Ok(new { status = "success", message = "Department added", data = dept });
        }

        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("departments")]
        public async Task<IActionResult> GetAll()
        {
            var depts = await _service.GetAll();
            if (depts == null || !depts.Any())
            {
                return NotFound(new { status = "error", message = "No departments found", data = new object() });
            }
            return Ok(new { status = "success", message = "Departments retrieved", data = depts });
        }

        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("department/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dept = await _service.GetById(id);
            if (dept == null)
            {
                return NotFound(new { status = "error", message = "Department not found", data = new object() });
            }
            return Ok(new { status = "success", message = "Department found", data = dept });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("department/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentDTO dto)
        {
            var updated = await _service.UpdateDept(id, dto);
            if (updated == null)
            {
                return NotFound(new { status = "error", message = "Department not found", data = new object() });
            }
            return Ok(new { status = "success", message = "Department updated", data = updated });
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("department/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteDept(id);
            if (!deleted)
            {
                return NotFound(new { status = "error", message = "Department not found", data = new object() });
            }
            return Ok(new { status = "success", message = "Department deleted", data = new object() });
        }
    }
}
