using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Business.Concrete;
using ToDoAPI.Entities.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // GET: api/status
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _statusService.GetAllStatusesAsync();
            return Ok(categories); 
        }

        // GET: api/status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetstatusById(int id)
        {
            var status = await _statusService.GetStatusByIdAsync(id);
            if (status == null)
            {
                return NotFound(); 
            }
            return Ok(status);
        }

        // POST: api/status
        [HttpPost]
        public async Task<IActionResult> Createstatus([FromBody] StatusDto statusDto)
        {
            if (statusDto == null)
            {
                return BadRequest("status verisi geçersiz.");
            }

            var createdstatus = await _statusService.CreateStatusAsync(statusDto);
            return CreatedAtAction(nameof(GetstatusById), new { id = createdstatus.StatusId }, createdstatus); 
        }

        // PUT: api/status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatestatus(int id, [FromBody] StatusDto statusDto)
        {
            if (id != statusDto.StatusId)
            {
                return BadRequest("status ID'leri eşleşmiyor.");
            }

            var updatedstatus = await _statusService.UpdateStatusAsync(statusDto);
            if (updatedstatus == null)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }

        // DELETE: api/status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestatus(int id)
        {
            var deleted = await _statusService.DeleteStatusAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent(); 
        }
    }
}
