using HairScheduler.Domain.Entities;

namespace HairScheduler.Domain.Repositories.Interfaces;
public interface IUpdateOnlyRepository
{
    Task<Schedule?> GetByNickname(string nickname);

    void Update(Schedule schedule);

}
