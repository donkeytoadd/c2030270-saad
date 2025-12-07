namespace c2030270_saad.Middleware.Interfaces
{

    public interface IJwtMiddleware
    {
        string GenerateToken(int userId, int tenantId, string email, string role);
    }
}