namespace c2030270_saad.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class NotificationController : ControllerBase 
    {
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(
            ILogger<NotificationController> logger)
        {
            this._logger = logger;
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
                _logger.LogError(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }

        
    }
}