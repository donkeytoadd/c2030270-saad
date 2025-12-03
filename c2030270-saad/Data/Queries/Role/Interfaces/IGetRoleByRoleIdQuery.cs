namespace c2030270_saad.Data.Queries.Role.Interfaces
{
    using Entities;

    public interface IGetRoleByRoleIdQuery
    {
        Role? Execute(int roleId);
    }
}