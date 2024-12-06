using HairScheduler.Communication.Requests;

namespace HairScheduler.Application.UseCases.Schedules.Update;
public interface IUpdateUseCase
{
    public Task Execute(string nickname, RequestSchedule request);

}
