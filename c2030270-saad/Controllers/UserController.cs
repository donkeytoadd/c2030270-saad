namespace c2030270_saad.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class UserController : ControllerBase 
    {
        private readonly ILogger<UserController> _logger;

        public UserController(
            ILogger<UserController> logger)
        {
            this._logger = logger;
        }

        [HttpPost("UserTest")]
        public async Task<IActionResult> UserTest()
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