namespace c2030270_saad.Middleware
{
    using Interfaces;
    using System.Security.Cryptography;
    using Data;
    using Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class RefreshTokenMiddleware : IRefreshTokenMiddleware
    {

        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private const int RefreshTokenDays = 7;

        public RefreshTokenMiddleware(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        private string GenerateSecureToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        public RefreshToken CreateToken(int userId, int roleId)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var token = new RefreshToken
                {
                    UserId = userId,
                    RoleId = roleId,
                    Token = GenerateSecureToken(),
                    CreatedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddDays(RefreshTokenDays)
                };

                context.RefreshToken.Add(token);
                context.SaveChanges();

                return token;
            }
        }


        public Task<(bool IsValid, RefreshToken? Token)> Validate(string token)
        {
            using (var context = contextFactory.CreateDbContext())
            {

                var rt = context.RefreshToken
                    .FirstOrDefault(x => x.Token == token);

                if (rt == null) return Task.FromResult<(bool IsValid, RefreshToken? Token)>((false, null));
                if (rt.RevokedAt != null) return Task.FromResult((false, rt));
                if (rt.ExpiresAt <= DateTime.Now) return Task.FromResult((false, rt));

                return Task.FromResult((true, rt));
            }
        }

        public Task Invalidate(RefreshToken token, string? replacedBy = null)
        {
            using (var context = contextFactory.CreateDbContext()){

                var existing = context.RefreshToken
                    .FirstOrDefault(x => x.RefreshTokenId == token.RefreshTokenId);

                if (existing != null)
                {
                    existing.RevokedAt = DateTime.Now;
                    existing.ReplacedByToken = replacedBy;
                    context.SaveChanges();
                }
            }
            return Task.CompletedTask;
        }
    }
}