namespace c2030270_saad.Business.Getters.Staff
{
    using Data.Entities;
    using Data.Queries.Staff.Interfaces;
    using Interfaces;

    public class StaffGetter : IStaffGetter
    {
        private readonly IGetStaffByStaffIdQuery getStaffByStaffIdQuery;
        
        public StaffGetter(IGetStaffByStaffIdQuery getStaffByStaffIdQuery)
        {
            this.getStaffByStaffIdQuery = getStaffByStaffIdQuery;
        }

        public Staff GetStaffByStaffId(int staffId)
        {
            var staff = this.getStaffByStaffIdQuery.Execute(staffId);

            return staff ?? new Staff();
        }
    }
}