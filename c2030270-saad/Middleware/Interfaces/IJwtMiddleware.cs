namespace c2030270_saad.Middleware.Interfaces
{

    public interface IJwtMiddleware
    {
        string GenerateToken(int userId, string email, string role);
    }
}