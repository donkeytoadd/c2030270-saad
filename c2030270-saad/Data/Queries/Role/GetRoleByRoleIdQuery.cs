namespace c2030270_saad.Data.Queries.Role
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetRoleByRoleIdQuery : IGetRoleByRoleIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetRoleByRoleIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Role? Execute(int roleId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Role.FirstOrDefault(role => role.RoleId == roleId);
            }
        }
    }
}