using Dal.Entities;
using Dal.TaskResult.Entities;
using Dal.Tasks.Entities;
using Dal.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public sealed class DataContext : IdentityDbContext<UserDal>
{
    public DbSet<HistoryDal> History { get; set; }
    public DbSet<TaskDal> Task { get; set; }
    public DbSet<BossTaskResultDal> BossTaskResults { get; set; }
    public DbSet<UserTaskResultDal> UserTaskResults { get; set; }
    public DbSet<BlockDal> Block { get; set; }
    public DbSet<DepartamentDal> Departament { get; set; }
    public DbSet<TaskResultDal> TaskResult { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    public DataContext()
    {
        Database.EnsureDeleted();   // удаляем бд со старой схемой
        Database.EnsureCreated();   // создаем бд с новой схемой        
    }
    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }

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

        builder.Entity<UserDal>().HasOne(u => u.Departament);
        builder.Entity<UserDal>().Property(p => p.Name).IsRequired(false);
        builder.Entity<UserDal>().Property(p => p.Surname).IsRequired(false);
        builder.Entity<UserDal>().Property(p => p.Patronymic).IsRequired(false);
    }
}