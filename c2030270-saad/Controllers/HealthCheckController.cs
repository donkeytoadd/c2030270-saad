namespace c2030270_saad.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    [ApiController] 
    [Route("api/[controller]/")]
    public class HealthCheckController : ControllerBase 
    {
        private readonly ILogger<HealthCheckController> logger;
        private readonly HealthCheckService _healthCheckService;

        public HealthCheckController(
            ILogger<HealthCheckController> logger, HealthCheckService healthCheckService)
        {
            this.logger = logger;
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthCheckReport()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            logger.LogInformation($"Performing health check: {report}");

            return report.Status == HealthStatus.Healthy ? Ok(report) : StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }

        
    }
}