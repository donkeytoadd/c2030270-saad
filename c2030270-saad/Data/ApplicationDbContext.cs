namespace c2030270_saad.Data
{
    using System.Reflection;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<Consumer> Consumer { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<ComplaintStatusHistory>  ComplaintStatusHistory { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<ComplaintAttachment> ComplaintAttachment { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}