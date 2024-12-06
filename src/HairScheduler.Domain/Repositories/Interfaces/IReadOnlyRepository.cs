using HairScheduler.Domain.Entities;

namespace HairScheduler.Domain.Repositories.Interfaces;
public interface IReadOnlyRepository
{
    public Task<List<Schedule>> GetAll();

    public Task<Schedule?> GetByNick(string nickname);

    public Task<List<Schedule>> FilterByDay(DateTime day, string nickname);


}
