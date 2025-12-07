namespace c2030270_saad.Data.Queries.Staff.Interfaces
{
    using Entities;

    public interface IGetStaffByStaffEmailQuery
    {
        List<Staff> Execute(string staffEmail);
    }
}