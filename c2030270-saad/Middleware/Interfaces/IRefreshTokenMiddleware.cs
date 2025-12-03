namespace c2030270_saad.Middleware.Interfaces
{
    using Data.Entities;

    public interface IRefreshTokenMiddleware
    {
        RefreshToken CreateToken(int userId, int roleId);
        Task<(bool IsValid, RefreshToken? Token)> Validate(string token);
        Task Invalidate(RefreshToken token, string? replacedBy = null);
    }
}