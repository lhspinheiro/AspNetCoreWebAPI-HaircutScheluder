using HairScheduler.Communication.Responses;

namespace HairScheduler.Application.UseCases.Schedules.GetByNick;
public interface IGetSchedulingByNickUseCase
{

    public Task<ResponseByNickname> Execute(string nickname);
}
