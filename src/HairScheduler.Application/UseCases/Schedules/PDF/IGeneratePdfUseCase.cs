namespace HairScheduler.Application.UseCases.Schedules.PDF;
public interface IGeneratePdfUseCase
{

    public Task<byte[]> Execute(DateTime day, string nickname);
}
