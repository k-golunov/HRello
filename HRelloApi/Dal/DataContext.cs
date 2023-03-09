using Dal.Email.Entities;
using Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class DataContext : IdentityDbContext<UserDal>
{
    
    public DbSet<EmailDal> Email { get; set; }
    
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserDal>(entity => entity.ToTable(name: "Users"));
        builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
        builder.Entity<IdentityUserRole<string>>(entity =>
            entity.ToTable(name: "UserRoles"));
        builder.Entity<IdentityUserClaim<string>>(entity =>
            entity.ToTable(name: "UserClaim"));
        builder.Entity<IdentityUserLogin<string>>(entity =>
            entity.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<string>>(entity =>
            entity.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<string>>(entity =>
            entity.ToTable("RoleClaims"));

        builder.ApplyConfiguration(new AuthConfiguration());
        
        builder.Entity<EmailDal>().HasIndex(u => u.Email).IsUnique();

        builder.Entity<UserDal>().HasOne(u => u.Departament);
        builder.Entity<UserDal>().Property(p => p.Name).IsRequired(false);
        builder.Entity<UserDal>().Property(p => p.Surname).IsRequired(false);
        builder.Entity<UserDal>().Property(p => p.Patronymic).IsRequired(false);
    }
}