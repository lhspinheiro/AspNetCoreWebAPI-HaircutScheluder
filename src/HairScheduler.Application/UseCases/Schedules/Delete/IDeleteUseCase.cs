namespace HairScheduler.Application.UseCases.Schedules.Delete;
public interface IDeleteUseCase
{
    public Task Execute(string nickname, DateTime date);
}
