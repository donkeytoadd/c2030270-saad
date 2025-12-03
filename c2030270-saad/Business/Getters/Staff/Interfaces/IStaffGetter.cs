namespace c2030270_saad.Business.Getters.Staff.Interfaces
{
    using Data.Entities;

    public interface IStaffGetter
    {
        Staff GetStaffByStaffId(int staffId);
        Staff GetStaffByStaffEmail(string email);
    }
}