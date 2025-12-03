namespace c2030270_saad.Business.Creators.Staff
{
    using Data.Entities;
    using Data.Enums;
    using Data.Queries.Role.Interfaces;
    using Data.Queries.Staff.Interfaces;
    using Interfaces;
    using Resources.Staff.Request;

    public class StaffCreator : IStaffCreator
    {
        private readonly IGetStaffByStaffEmailQuery getStaffByStaffEmailQuery;
        private readonly IGetRoleByRoleNameQuery getRoleByRoleNameQuery;
        private readonly ICreateStaffQuery createStaffQuery;

        public StaffCreator(
            ICreateStaffQuery createStaffQuery,
            IGetStaffByStaffEmailQuery getStaffByStaffEmailQuery,
            IGetRoleByRoleNameQuery getRoleByRoleNameQuery)
        {
            this.createStaffQuery = createStaffQuery;
            this.getStaffByStaffEmailQuery = getStaffByStaffEmailQuery;
            this.getRoleByRoleNameQuery = getRoleByRoleNameQuery;
        }

        public Staff CreateStaff(CreateStaffRequest createStaffRequest, RoleEnum roleEnum)
        {
            try
            {
                var staffExists = this.getStaffByStaffEmailQuery.Execute(createStaffRequest.Email);
                var role = this.getRoleByRoleNameQuery.Execute(roleEnum.ToString());
                
                if (staffExists == null && role != null)
                {
                    var mappedStaff = new Staff()
                    {
                        TenantId =  createStaffRequest.TenantId,
                        RoleId =  role.RoleId,
                        FName =  createStaffRequest.FName,
                        LName =  createStaffRequest.LName,
                        Email = createStaffRequest.Email,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(createStaffRequest.Password),
                        ContactNumber = createStaffRequest.ContactNumber,
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    };
                    
                    var staff = this.createStaffQuery.Execute(mappedStaff);
                    return staff;
                }
                
                throw new Exception("Staff already exists.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}