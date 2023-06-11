using Dal.Base;
using Dal.Entities;
using Dal.User.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dal.User.Repositories;

/// <summary>
/// Репозиторий, который должен быть реализацией над айдентити
/// В данные момент не реализовано.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    protected readonly DbSet<UserDal> _dbSet;

    public UserRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<UserDal>();
    }

    public async Task ChangePasswordAsync(UserDal dal)
    {
        _context.Entry(dal).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetDepartmentId(string userId)
    {
        var entity = await _dbSet.Include(x => x.Departament).FirstAsync(x => x.Id == userId);
        var departmentId = entity.Departament.Id;
        return departmentId;
    }
}