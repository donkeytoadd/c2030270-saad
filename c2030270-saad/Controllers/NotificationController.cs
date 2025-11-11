namespace c2030270_saad.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class NotificationController : ControllerBase 
    {
        private readonly ILogger<NotificationController> logger;

        public NotificationController(
            ILogger<NotificationController> logger)
        {
            this.logger = logger;
        }

        [HttpPost("NotificationTest")]
        public async Task<IActionResult> NotificationTest()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }

        
    }
}