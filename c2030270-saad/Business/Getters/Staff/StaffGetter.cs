namespace c2030270_saad.Business.Getters.Staff
{
    using Data.Entities;
    using Data.Queries.Staff.Interfaces;
    using Interfaces;

    public class StaffGetter : IStaffGetter
    {
        private readonly IGetStaffByStaffIdQuery getStaffByStaffIdQuery;
        private readonly IGetStaffByStaffEmailQuery getStaffByStaffEmailQuery;
        
        public StaffGetter(
            IGetStaffByStaffIdQuery getStaffByStaffIdQuery,
            IGetStaffByStaffEmailQuery getStaffByStaffEmailQuery)
        {
            this.getStaffByStaffIdQuery = getStaffByStaffIdQuery;
            this.getStaffByStaffEmailQuery = getStaffByStaffEmailQuery;
        }

        public Staff GetStaffByStaffId(int staffId)
        {
            var staff = this.getStaffByStaffIdQuery.Execute(staffId);

            return staff ?? new Staff();
        }

        public Staff GetStaffByStaffEmail(string staffEmail)
        {
            var staff = this.getStaffByStaffEmailQuery.Execute(staffEmail);
            return staff ?? new Staff();
        }
    }
}