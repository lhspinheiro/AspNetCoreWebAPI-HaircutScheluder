using HairScheduler.Domain.Entities;
using HairScheduler.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HairScheduler.Infrastructure.Data.Repositories;

internal class Repository : IWriteOnlyRepository, IReadOnlyRepository, IUpdateOnlyRepository, IDeleteRepository
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public async Task Add(Schedule schedule)
    {
        await _dbContext.Schedules.AddAsync(schedule);
    }

    public async Task<bool> Delete(string nickname, DateTime date)
    {
        var remove = await _dbContext.Schedules.FirstOrDefaultAsync(r => r.Nickname == nickname && r.Date == date);
        if (remove is null)
        {
            return false;
        }

        _dbContext.Schedules.Remove(remove);
        return true;
    }

    public async Task<bool> ExistData(DateTime date)
    {
        date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0); 

        var start = date.AddMinutes(-29);
        var end = date.AddMinutes(29);

        return await _dbContext.Schedules.AnyAsync(d => d.Date >= start && d.Date <= end);
    }

    public async Task<List<Schedule>> FilterByDay(DateTime day, string nickname)
    {
     
        var startDate = day.Date; 
        var endDate = day.Date.AddHours(23).AddMinutes(59).AddSeconds(59); 

        return await _dbContext.Schedules.AsNoTracking()
            .Where(d => d.Date >= startDate && d.Date <= endDate)
            .OrderBy(d => d.Date)
            .Where(d => d.Nickname == nickname)
            .ToListAsync();

    }
    public async Task<List<Schedule>> GetAll()
    {
        return await _dbContext.Schedules.AsNoTracking().ToListAsync();
    }

    public async Task<Schedule?> GetByNickname(string nickname)
    {
        return await _dbContext.Schedules.FirstOrDefaultAsync(n => n.Nickname == nickname);
    }

    public void Update(Schedule schedule)
    {
        _dbContext.Schedules.Update(schedule);
    }

    async Task<Schedule?> IReadOnlyRepository.GetByNick(string nickname)
    {
        return await _dbContext.Schedules.AsNoTracking().FirstOrDefaultAsync(n => n.Nickname == nickname);
    }
}
