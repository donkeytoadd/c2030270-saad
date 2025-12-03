namespace c2030270_saad.Data.Queries.Role.Interfaces
{
    using c2030270_saad.Data.Entities;
    
    public interface IGetRoleByRoleNameQuery
    {
        Role? Execute(string roleName);
    }
}