namespace HairScheduler.Domain.Repositories.Interfaces;
public interface IDeleteRepository
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns></returns>
    public Task<bool> Delete(string nickname, DateTime date);

}
