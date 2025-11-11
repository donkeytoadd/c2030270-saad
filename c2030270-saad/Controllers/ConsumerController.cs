namespace c2030270_saad.Controllers
{
    using Business.Getters.Consumer.Interfaces;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class ConsumerController : ControllerBase 
    {
        private readonly ILogger<ConsumerController> logger;
        private readonly IConsumerGetter consumerGetter;

        public ConsumerController(
            ILogger<ConsumerController> logger,
            IConsumerGetter consumerGetter)
        {
            this.logger = logger;
            this.consumerGetter = consumerGetter;
        }

        [HttpGet("GetConsumersByTenantId")]
        public async Task<ActionResult<List<Consumer>>> GetAllConsumersByTenantId(int tenantId)
        {
            try
            {
                logger.LogInformation($"Getting all complaints for tenant with tenantId {tenantId}");
    
                var complaintList = this.consumerGetter.GetConsumersByTenantId(tenantId);
                return Ok(complaintList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving consumers from tenant with tenantId {tenantId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}