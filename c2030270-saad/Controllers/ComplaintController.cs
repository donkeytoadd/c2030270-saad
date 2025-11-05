namespace c2030270_saad.Controllers
{
    using Business.Creators.Complaint.Interfaces;
    using Business.Getters.Complaint.Interfaces;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Resources.Complaint.Request;

    [ApiController] 
    [Route("api/[controller]/")]
    public class ComplaintController : ControllerBase 
    {
        private readonly ILogger<ComplaintController> _logger;
        private readonly IComplaintGetter complaintGetter;
        private readonly IComplaintCreator complaintCreator;

        public ComplaintController(
            ILogger<ComplaintController> logger,
            IComplaintGetter complaintGetter,
            IComplaintCreator complaintCreator)
        {
            this._logger = logger;
            this.complaintGetter = complaintGetter;
            this.complaintCreator = complaintCreator;
        }

        [HttpGet("GetComplaint")]
        public async Task<ActionResult<Complaint>> GetComplaint(int complaintId)
        {
            try
            {
                _logger.LogInformation("Getting complaint with ID {ComplaintId}", complaintId);
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
                _logger.LogError(ex, "Error retrieving complaint with ID {ComplaintId}", complaintId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllComplaints")]
        public async Task<ActionResult<List<Complaint>>> GetAllComplaints(int consumerId)
        {
            try
            {
                _logger.LogInformation("Getting all complaints for ConsumerId {ConsumerId}", consumerId);
    
                var complaintList = this.complaintGetter.GetAllComplaints(consumerId);
                return Ok(complaintList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving complaints from account with ConsumerId {consumerId}", consumerId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddComplaint")]
        public async Task<ActionResult> AddComplaint([FromBody] AddComplaintRequest complaintRequest)
        {
            try
            {
                _logger.LogInformation("Creating new complaint for ConsumerId: {ConsumerId}", complaintRequest.ConsumerId);

                var complaint = this.complaintCreator.CreateComplaint(complaintRequest);

                return Ok(complaint);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating complaint for ConsumerId {ConsumerId}", complaintRequest.ConsumerId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}