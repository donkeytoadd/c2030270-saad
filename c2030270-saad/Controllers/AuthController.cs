namespace c2030270_saad.Controllers
{
    using Business.Getters.Staff.Interfaces;
    using c2030270_saad.Business.Getters.Consumer.Interfaces;
    using Data.Queries.Role.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Middleware.Interfaces;
    using Resources.Auth.Request;
    using Resources.Auth.Response;

    [ApiController] 
    [Route("api/[controller]/")]
    public class AuthController : ControllerBase 
    {
        private readonly ILogger<AuthController> logger;
        private readonly IJwtMiddleware jwtMiddleware;
        private readonly IRefreshTokenMiddleware refreshTokenMiddleware;
        private readonly IConsumerGetter consumerGetter;
        private readonly IStaffGetter staffGetter;
        private readonly IGetRoleByRoleIdQuery getRoleByRoleIdQuery;

        public AuthController(
            ILogger<AuthController> logger,
            IJwtMiddleware jwtMiddleware,
            IRefreshTokenMiddleware refreshTokenMiddleware,
            IConsumerGetter consumerGetter,
            IStaffGetter staffGetter,
            IGetRoleByRoleIdQuery getRoleByRoleIdQuery
            )
        {
            this.logger = logger;
            this.jwtMiddleware = jwtMiddleware;
            this.refreshTokenMiddleware = refreshTokenMiddleware;
            this.consumerGetter = consumerGetter;
            this.staffGetter = staffGetter;
            this.getRoleByRoleIdQuery = getRoleByRoleIdQuery;
        }
        
        [HttpGet("FindTenants")]
        [AllowAnonymous]
        public IActionResult FindTenants([FromQuery] TenantLookupRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Email is required.");

            var tenants = new List<TenantLookupResponse>();

            var consumerList = this.consumerGetter.GetConsumerByEmail(request.Email);
            if (consumerList != null)
            {
                tenants.AddRange(consumerList.Select(c => new TenantLookupResponse
                {
                    TenantId = c.TenantId,
                    TenantName = c.Tenant.Name,
                    Role = "Consumer"
                }));
            }

            var staffList = this.staffGetter.GetStaffByStaffEmail(request.Email);
            
            if (staffList != null)
            {
                tenants.AddRange(staffList.Select(s => new TenantLookupResponse
                {
                    TenantId = s.TenantId,
                    TenantName = s.Tenant.Name,
                    Role = "Staff"
                }));
            }

            if (tenants.Count == 0)
                return NotFound("No accounts associated with this email.");

            return Ok(tenants);
        }
        
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || request.TenantId == 0)
            {
                return BadRequest("Email, password, and tenantId are required.");
            }

            var tenantId = request.TenantId;

            var consumers = this.consumerGetter.GetConsumerByEmail(request.Email);
            var consumer = consumers?.FirstOrDefault(c => c.TenantId == tenantId);

            if (consumer != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, consumer.PasswordHash))
                    return Unauthorized("Invalid password.");

                var role = getRoleByRoleIdQuery.Execute(consumer.RoleId);

                var jwt = jwtMiddleware.GenerateToken(
                    consumer.ConsumerId,
                    consumer.TenantId,
                    consumer.Email,
                    role.RoleName
                );

                var refresh = refreshTokenMiddleware.CreateToken(
                    consumer.ConsumerId,
                    consumer.TenantId,
                    role.RoleId
                );

                return Ok(new LoginResponse
                {
                    Token = jwt,
                    RefreshToken = refresh.Token,
                    UserId = consumer.ConsumerId,
                    Role = role.RoleName,
                    TenantId = consumer.TenantId
                });
            }

            var staffList = this.staffGetter.GetStaffByStaffEmail(request.Email);
            var staff = staffList?.FirstOrDefault(s => s.TenantId == tenantId);

            if (staff != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, staff.PasswordHash))
                    return Unauthorized("Invalid password.");

                var role = getRoleByRoleIdQuery.Execute(staff.RoleId);

                var jwt = jwtMiddleware.GenerateToken(
                    staff.StaffId,
                    staff.TenantId,
                    staff.Email,
                    role.RoleName
                );

                var refresh = refreshTokenMiddleware.CreateToken(
                    staff.StaffId,
                    staff.TenantId,
                    role.RoleId
                );

                return Ok(new LoginResponse
                {
                    Token = jwt,
                    RefreshToken = refresh.Token,
                    UserId = staff.StaffId,
                    Role = role.RoleName,
                    TenantId = staff.TenantId
                });
            }

            return Unauthorized("Account not found for this tenant.");
        }
        
        [HttpPost("Refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var (isValid, storedToken) = await refreshTokenMiddleware.Validate(request.RefreshToken);

            if (!isValid || storedToken == null)
                return Unauthorized("Invalid or expired refresh token.");

            int userId = storedToken.UserId;
            int tenantId = storedToken.TenantId;

            var role = getRoleByRoleIdQuery.Execute(storedToken.RoleId);
            string email;

            if (role.RoleName == "Consumer")
            {
                var consumer = consumerGetter.GetConsumerByConsumerId(userId, tenantId);
                if (consumer.ConsumerId == 0)
                    return Unauthorized("User no longer exists.");
                email = consumer.Email;
            }
            else
            {
                var staff = staffGetter.GetStaffByStaffId(userId, tenantId);
                if (staff.StaffId == 0)
                    return Unauthorized("User no longer exists.");
                email = staff.Email;
            }

            refreshTokenMiddleware.Invalidate(storedToken);

            var newRefresh = refreshTokenMiddleware.CreateToken(userId, tenantId, storedToken.RoleId);
            var newJwt = jwtMiddleware.GenerateToken(userId, tenantId, email, role.RoleName);

            return Ok(new LoginResponse
            {
                Token = newJwt,
                RefreshToken = newRefresh.Token,
                UserId = userId,
                Role = role.RoleName,
                TenantId = tenantId
            });
        }
    }
}