namespace c2030270_saad.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class AuthController : ControllerBase 
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ILogger<AuthController> logger)
        {
            this._logger = logger;
        }

        [HttpPost("AuthTest")]
        public async Task<IActionResult> AuthTest()
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