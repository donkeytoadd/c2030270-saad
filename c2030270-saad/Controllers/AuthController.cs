namespace c2030270_saad.Controllers
{
    using Business.Getters.Staff.Interfaces;
    using c2030270_saad.Business.Getters.Consumer.Interfaces;
    using c2030270_saad.Data.Entities;
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var consumer = this.consumerGetter.GetConsumerByEmail(request.Email);

            Staff? staff = null;

            if (consumer.ConsumerId == 0)
            {
                staff = this.staffGetter.GetStaffByStaffEmail(request.Email);
            }

            if (consumer.ConsumerId != 0)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, consumer.PasswordHash))
                    return Unauthorized("Invalid email or password.");

                var role = this.getRoleByRoleIdQuery.Execute(consumer.RoleId);

                var jwt = this.jwtMiddleware.GenerateToken(consumer.ConsumerId, consumer.Email, role.RoleName);

                var refresh = this.refreshTokenMiddleware.CreateToken(consumer.ConsumerId, role.RoleId);

                return Ok(new LoginResponse
                {
                    Token = jwt,
                    RefreshToken = refresh.Token,
                    UserId = consumer.ConsumerId,
                    Role = role.RoleName
                });
            }

            if (staff != null && staff.StaffId != 0)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, staff.PasswordHash))
                    return Unauthorized("Invalid email or password.");

                var role = this.getRoleByRoleIdQuery.Execute(staff.RoleId);

                var jwt = this.jwtMiddleware.GenerateToken(staff.StaffId, staff.Email, role.RoleName);

                var refresh = this.refreshTokenMiddleware.CreateToken(staff.StaffId, role.RoleId);

                return Ok(new LoginResponse
                {
                    Token = jwt,
                    RefreshToken = refresh.Token,
                    UserId = staff.StaffId,
                    Role = role.RoleName
                });
            }
            
            return Unauthorized("Invalid email or password.");
        }
        
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var (isValid, storedToken) = await refreshTokenMiddleware.Validate(request.RefreshToken);

            if (!isValid || storedToken == null)
                return Unauthorized("Invalid or expired refresh token.");

            string email;
            string roleName;
            int userId = storedToken.UserId;

            var role = this.getRoleByRoleIdQuery.Execute(storedToken.RoleId);

            if (role.RoleName == "Consumer")
            {
                var consumer = consumerGetter.GetConsumerByConsumerId(userId);
                if (consumer.ConsumerId == 0) return Unauthorized("User no longer exists.");
                email = consumer.Email;
            }
            else
            {
                var staff = staffGetter.GetStaffByStaffId(userId);
                if (staff.StaffId == 0) return Unauthorized("User no longer exists.");
                email = staff.Email;
            }

            // Invalidate old token
            refreshTokenMiddleware.Invalidate(storedToken);

            // Create new rotated refresh token
            var newRefreshToken = this.refreshTokenMiddleware.CreateToken(userId, storedToken.RoleId);

            // Create new JWT
            var newJwt = this.jwtMiddleware.GenerateToken(userId, email, role.RoleName);

            return Ok(new LoginResponse
            {
                Token = newJwt,
                RefreshToken = newRefreshToken.Token,
                Role = role.RoleName,
                UserId = userId
            });
        }
    }
}