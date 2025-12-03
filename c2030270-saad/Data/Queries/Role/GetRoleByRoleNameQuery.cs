namespace c2030270_saad.Data.Queries.Role
{
    using c2030270_saad.Data.Queries.Role.Interfaces;
    using c2030270_saad.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class GetRoleByRoleNameQuery : IGetRoleByRoleNameQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetRoleByRoleNameQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public Role? Execute(string roleName)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Role.FirstOrDefault(x=>x.RoleName == roleName);
            }
        }
    }
}