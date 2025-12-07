namespace c2030270_saad.Controllers
{
    using c2030270_saad.Resources.Consumer;
    using Business.Creators.Consumer.Interfaces;
    using Business.Getters.Consumer.Interfaces;
    using Data.Entities;
    using Data.Queries.Consumer.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class ConsumerController : ControllerBase 
    {
        private readonly ILogger<ConsumerController> logger;
        private readonly IConsumerGetter consumerGetter;
        private readonly IConsumerCreator consumerCreator;
        private readonly ISearchForConsumerQuery searchForConsumerQuery;

        public ConsumerController(
            ILogger<ConsumerController> logger,
            IConsumerGetter consumerGetter,
            IConsumerCreator consumerCreator,
            ISearchForConsumerQuery searchForConsumerQuery)
        {
            this.logger = logger;
            this.consumerGetter = consumerGetter;
            this.consumerCreator = consumerCreator;
            this.searchForConsumerQuery = searchForConsumerQuery;
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

        [HttpGet("GetConsumerByConsumerId")]
        public async Task<ActionResult<Consumer>> GetConsumerByConsumerId(int consumerId)
        {
            try
            {
                logger.LogInformation($"Getting consumer details for consumer with consumerId {consumerId}");
                
                int tenantId = int.Parse(User.FindFirst("tenantId")!.Value);

                var consumer = this.consumerGetter.GetConsumerByConsumerId(consumerId, tenantId);
                return Ok(consumer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving consumer details for consumer with consumerId {consumerId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("CreateConsumer")]
        public async Task<ActionResult<Consumer>> CreateConsumer([FromBody] CreateConsumerRequest consumerRequest)
        {
            try
            {
                logger.LogInformation("Creating a new consumer");
                
                var consumer = this.consumerCreator.CreateConsumer(consumerRequest);
                return Ok(consumer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error creating new consumer");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("search")]
        public ActionResult<List<SearchConsumerResult>> SearchConsumers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Ok(new List<SearchConsumerResult>());

            var results = this.searchForConsumerQuery.Execute(query);

            return Ok(results);
        }
    }
}