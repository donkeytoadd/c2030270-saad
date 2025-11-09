namespace c2030270_saad.Controllers
{
    using Business.Getters.Tenant.Interfaces;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController] 
    [Route("api/[controller]/")]
    public class TenantController : ControllerBase 
    {
        private readonly ILogger<TenantController> _logger;
        private readonly ITenantGetter tenantGetter;

        public TenantController(
            ILogger<TenantController> logger,
            ITenantGetter tenantGetter)
        {
            this._logger = logger;
            this.tenantGetter = tenantGetter;
        }

        [HttpPost("CreateNewTenant")]
        public async Task<ActionResult<Tenant>> CreateNewTenant()
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

        [HttpGet("GetAllTenants")]
        public async Task<ActionResult<List<Tenant>>> GetAllTenants()
        {
            try
            {
                var tenantList = this.tenantGetter.GetAllTenants();
                return Ok(tenantList);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tenant list");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetTenantById")]
        public async Task<ActionResult<Tenant>> GetTenantById(int tenantId)
        {
            try
            {
                var tenant = this.tenantGetter.GetTenantByTenantId(tenantId);
                return Ok(tenant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tenant with tenant id {tenantId}");
                return StatusCode(500, "Internal server error");
            }
        }
        
    }
}