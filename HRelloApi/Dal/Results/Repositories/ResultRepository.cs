﻿using Dal.Base;
using Dal.Results.Repositories.Interfaces;
using Dal.TaskResult.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Results.Repositories;

/// <summary>
/// Репозиторий для работы с итогами от главного руководителя
/// </summary>
public class ResultRepository : BaseRepository<TaskResultDal, Guid>, IResultRepository
{
    public ResultRepository(DataContext context) : base(context)
    {
    }

    public override async Task<TaskResultDal?> GetAsync(Guid id)
    {
        return await _dbSet.Include(dal => dal.Tasks)
            .FirstAsync(x => x.Id == id);
    }

    public override async Task<List<TaskResultDal>> GetAllAsync()
    {
        return await _dbSet.Include(dal => dal.Tasks)
            .ThenInclude(x => x.Block)
            .ToListAsync();
    }

    public List<TaskResultDal> GetAllWhere(Func<TaskResultDal, bool> func)
    {
        IQueryable<TaskResultDal> results = _context.TaskResult
            .Include(x => x.Tasks)
            .ThenInclude(x => x.Block);
        return results.Where(func).ToList();
    }
}