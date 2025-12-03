namespace c2030270_saad.Business.Creators.Staff.Interfaces
{
    using Data.Entities;
    using Data.Enums;
    using Resources.Staff.Request;

    public interface IStaffCreator
    {
        Staff CreateStaff(CreateStaffRequest createStaffRequest, RoleEnum roleEnum);
    }
}