namespace c2030270_saad.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class TenantController : ControllerBase 
    {
        private readonly ILogger<TenantController> _logger;

        public TenantController(
            ILogger<TenantController> logger)
        {
            this._logger = logger;
        }

        [HttpPost("TenantTest")]
        public async Task<IActionResult> TenantTest()
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