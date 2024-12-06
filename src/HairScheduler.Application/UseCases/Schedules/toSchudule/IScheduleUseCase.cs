using HairScheduler.Communication.Requests;
using HairScheduler.Communication.Responses;

namespace HairScheduler.Application.UseCases.Schedules.toSchudule;
public interface IScheduleUseCase
{
    Task<HairScheduleResponse> Execute(RequestSchedule request);

}
