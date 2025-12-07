namespace c2030270_saad.Controllers
{
    using Business.Creators.Complaint.Interfaces;
    using Business.Getters.Complaint.Interfaces;
    using Business.Updaters.Complaint.Interfaces;
    using Data.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Resources.Complaint.Request;

    [ApiController] 
    [Route("api/[controller]/")]
    public class ComplaintController : ControllerBase 
    {
        private readonly ILogger<ComplaintController> logger;
        private readonly IComplaintGetter complaintGetter;
        private readonly IComplaintCreator complaintCreator;
        private readonly IComplaintStatusUpdater complaintStatusUpdater;

        public ComplaintController(
            ILogger<ComplaintController> logger,
            IComplaintGetter complaintGetter,
            IComplaintCreator complaintCreator,
            IComplaintStatusUpdater complaintStatusUpdater)
        {
            this.logger = logger;
            this.complaintGetter = complaintGetter;
            this.complaintCreator = complaintCreator;
            this.complaintStatusUpdater = complaintStatusUpdater;
        }
        
        private bool GetTenantId(out int tenantId)
        {
            tenantId = 0;
            var claim = User.FindFirst("tenantId")?.Value;

            return claim != null && int.TryParse(claim, out tenantId);
        }

        private bool GetUserId(out int userId)
        {
            userId = 0;
            var claim = User.FindFirst("userId")?.Value;

            return claim != null && int.TryParse(claim, out userId);
        }

        [HttpGet("GetComplaint")]
        [Authorize]
        public async Task<ActionResult<Complaint>> GetComplaint(int complaintId)
        {
            try
            {
                if (!GetTenantId(out var tenantId))
                    return Unauthorized("Tenant identification missing.");
                
                logger.LogInformation($"Getting complaint with ID {complaintId}");
                var complaint = this.complaintGetter.GetComplaint(complaintId, tenantId);
                
                if (complaint == null)
                {
                    logger.LogWarning($"Complaint with ID {complaintId} not found");
                    return BadRequest($"Complaint with ID {complaintId} not found");
                }
                
                return Ok(complaint);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving complaint with ID {complaintId}");
                return BadRequest($"Error retrieving complaint with ID {complaintId}");
            }
        }

        [HttpGet("GetComplaintsByConsumerId")]
        public async Task<ActionResult<List<Complaint>>> GetComplaintsByConsumerId(int consumerId)
        {
            try
            {
                if (!GetTenantId(out var tenantId))
                    return Unauthorized("Tenant identification missing.");
                
                logger.LogInformation($"Getting all complaints for ConsumerId {consumerId}");
                var complaintList = this.complaintGetter.GetComplaintsByConsumerId(consumerId, tenantId);
                
                return Ok(complaintList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving complaints from account with ConsumerId {consumerId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllTenantsComplaints")]
        public async Task<ActionResult<List<Complaint>>> GetComplaintsByTenantId(int tenantId)
        {
            try
            {
                logger.LogInformation($"Getting all complaints for tenant with tenantId {tenantId}");
                var complaintList = this.complaintGetter.GetComplaintsByTenantId(tenantId);
                
                return Ok(complaintList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving complaints for tenant with tenantId {tenantId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("CreateComplaint")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> CreateComplaint([FromBody] CreateComplaintRequest complaintRequest)
        {
            try
            {
                if (!GetTenantId(out var tenantId) || !GetUserId(out var userId))
                    return Unauthorized("User or tenant identification missing.");
                
                logger.LogInformation($"Creating new complaint for ConsumerId: {complaintRequest.ConsumerId}");
                var complaint = this.complaintCreator.CreateComplaint(complaintRequest, userId, tenantId);

                return Ok(complaint);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error creating complaint for ConsumerId {complaintRequest.ConsumerId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("UpdateComplaintStatus")]
        [Authorize]
        public async Task<ActionResult> UpdateComplaintStatus([FromBody] UpdateComplaintStatusRequest request)
        {
            try
            {
                if (request.ComplaintId <= 0 || string.IsNullOrWhiteSpace(request.NewStatus))
                    return BadRequest("Invalid request data.");
            
                if (!GetTenantId(out var tenantId) || !GetUserId(out var userId))
                    return Unauthorized("User or tenant identification missing.");
                
                var updatedComplaint = complaintStatusUpdater.UpdateComplaintStatus(request.ComplaintId, tenantId, request.NewStatus, userId);

                return Ok(updatedComplaint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update complaint status: {ex.Message}");
            }
        }
    }
}