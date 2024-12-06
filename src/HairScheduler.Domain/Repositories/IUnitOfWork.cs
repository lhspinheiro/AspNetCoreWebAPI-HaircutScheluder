namespace HairScheduler.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
