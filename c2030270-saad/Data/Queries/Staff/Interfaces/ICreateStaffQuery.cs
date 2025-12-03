namespace c2030270_saad.Data.Queries.Staff.Interfaces
{
    using Entities;
    using Resources.Staff.Request;

    public interface ICreateStaffQuery
    {
        Staff Execute(Staff staff);
    }
}