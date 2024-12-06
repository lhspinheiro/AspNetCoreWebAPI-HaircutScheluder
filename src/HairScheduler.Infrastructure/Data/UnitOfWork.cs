using HairScheduler.Domain.Repositories;

namespace HairScheduler.Infrastructure.Data;

internal class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        
    }
    public async Task Commit() => await _appDbContext.SaveChangesAsync();
}
