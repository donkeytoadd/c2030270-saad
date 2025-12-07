namespace c2030270_saad.Data.Queries.Staff.Interfaces
{
    using Entities;

    public interface IGetStaffByStaffIdQuery
    {
        Staff Execute(int staffId, int tenantId);
    }
}