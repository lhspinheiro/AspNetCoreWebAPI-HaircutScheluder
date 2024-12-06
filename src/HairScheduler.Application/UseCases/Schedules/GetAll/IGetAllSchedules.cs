using HairScheduler.Communication.Responses;

namespace HairScheduler.Application.UseCases.Schedules.GetAll;
public interface IGetAllSchedules
{
    public Task<ResponseGetAll> Execute();
}
