namespace c2030270_saad.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class ComplaintController : ControllerBase 
    {
        private readonly ILogger<ComplaintController> _logger;

        public ComplaintController(
            ILogger<ComplaintController> logger)
        {
            this._logger = logger;
        }

        [HttpPost("ComplaintTest")]
        public async Task<IActionResult> ComplaintTest()
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