using HairScheduler.Domain.Entities;

namespace HairScheduler.Domain.Repositories.Interfaces;
public interface IWriteOnlyRepository
{
    public Task Add(Schedule schedule);

  

}
