namespace c2030270_saad.Data.Queries.Staff.Interfaces
{
    using Entities;

    public interface IGetStaffByStaffEmailQuery
    {
        Staff? Execute(string staffEmail);
    }
}