namespace c2030270_saad.Controllers
{
    using Business.Creators.Staff.Interfaces;
    using Business.Getters.Staff.Interfaces;
    using Data.Entities;
    using Data.Enums;
    using Microsoft.AspNetCore.Mvc;
    using Resources.Staff.Request;

    [ApiController]
    [Route("api/[controller]/")]
    public class StaffController : ControllerBase
    {
        private readonly ILogger<StaffController> logger;
        private readonly IStaffGetter staffGetter;
        private readonly IStaffCreator staffCreator;

        public StaffController(
            ILogger<StaffController> logger,
            IStaffGetter staffGetter,
            IStaffCreator staffCreator)
        {
            this.logger = logger;
            this.staffGetter = staffGetter;
            this.staffCreator = staffCreator;
        }

        [HttpGet("GetStaffByStaffId")]
        public async Task<ActionResult<Staff>> GetStaffByStaffId(int staffId)
        {
            try
            {
                logger.LogInformation($"Getting Staff with ID {staffId}");
                int tenantId = int.Parse(User.FindFirst("tenantId")!.Value);
                
                var staff = this.staffGetter.GetStaffByStaffId(staffId, tenantId);

                return Ok(staff);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving Staff with ID {staffId}");
                return BadRequest($"Error retrieving Staff with ID {staffId}");
            }
        }

        [HttpPost("CreateStaff")]
        public async Task<ActionResult<Staff>> CreateStaff([FromBody] CreateStaffRequest createStaffRequest, [FromQuery] RoleEnum role)
        {
            try
            {
                logger.LogInformation($"Creating Staff with Role {role}");
                var staff = this.staffCreator.CreateStaff(createStaffRequest, role);
                
                return Ok(staff);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error creating Staff with Role {role}");
                return BadRequest($"Error creating Staff with Role {role}");
            }
        }
    }
}