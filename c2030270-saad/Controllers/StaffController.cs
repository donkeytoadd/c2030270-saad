namespace c2030270_saad.Controllers
{
    using Business.Getters.Consumer.Interfaces;
    using Business.Getters.Staff.Interfaces;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]/")]
    public class StaffController : ControllerBase
    {
        private readonly ILogger<StaffController> logger;
        private readonly IStaffGetter staffGetter;

        public StaffController(
            ILogger<StaffController> logger,
            IStaffGetter staffGetter)
        {
            this.logger = logger;
            this.staffGetter = staffGetter;
        }

        [HttpGet("GetStaffByStaffId")]
        public async Task<ActionResult<Staff>> GetStaffByStaffId(int staffId)
        {
            try
            {
                logger.LogInformation($"Getting Staff with ID {staffId}");
                var staff = this.staffGetter.GetStaffByStaffId(staffId);

                return Ok(staff);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving Staff with ID {staffId}");
                return BadRequest($"Error retrieving Staff with ID {staffId}");
            }
        }
    }
}