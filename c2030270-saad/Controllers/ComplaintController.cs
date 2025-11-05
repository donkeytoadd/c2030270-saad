namespace c2030270_saad.Controllers
{
    using Business.Helpers.Interfaces;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class ComplaintController : ControllerBase 
    {
        private readonly ILogger<ComplaintController> _logger;
        private readonly IComplaintGetter complaintGetter;

        public ComplaintController(
            ILogger<ComplaintController> logger,
            IComplaintGetter complaintGetter)
        {
            this._logger = logger;
            this.complaintGetter = complaintGetter;
        }

        [HttpGet("GetComplaint")]
        public async Task<ActionResult<Complaint>> GetComplaint(int complaintId)
        {
            try
            {
                _logger.LogInformation("Getting complaint with ID: {ComplaintId}", complaintId);
                var complaint = this.complaintGetter.GetComplaint(complaintId);
                
                if (complaint == null)
                {
                    _logger.LogWarning("Complaint with ID {ComplaintId} not found", complaintId);
                    return BadRequest($"Complaint with ID {complaintId} not found");
                }
                
                return Ok(complaint);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}