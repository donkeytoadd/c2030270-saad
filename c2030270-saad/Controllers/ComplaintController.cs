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
        private readonly ILogger<ComplaintController> logger;
        private readonly IComplaintGetter complaintGetter;
        private readonly IComplaintCreator complaintCreator;

        public ComplaintController(
            ILogger<ComplaintController> logger,
            IComplaintGetter complaintGetter,
            IComplaintCreator complaintCreator)
        {
            this.logger = logger;
            this.complaintGetter = complaintGetter;
            this.complaintCreator = complaintCreator;
        }

        [HttpGet("GetComplaint")]
        public async Task<ActionResult<Complaint>> GetComplaint(int complaintId)
        {
            try
            {
                logger.LogInformation($"Getting complaint with ID {complaintId}");
                var complaint = this.complaintGetter.GetComplaint(complaintId);
                
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
                logger.LogInformation($"Getting all complaints for ConsumerId {consumerId}");
    
                var complaintList = this.complaintGetter.GetComplaintsByConsumerId(consumerId);
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

        [HttpPost("AddComplaint")]
        public async Task<ActionResult> AddComplaint([FromBody] CreateComplaintRequest complaintRequest)
        {
            try
            {
                logger.LogInformation($"Creating new complaint for ConsumerId: {complaintRequest.ConsumerId}");

                var complaint = this.complaintCreator.CreateComplaint(complaintRequest);

                return Ok(complaint);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error creating complaint for ConsumerId {complaintRequest.ConsumerId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}