using HairScheduler.Domain.Repositories;
using HairScheduler.Domain.Repositories.Interfaces;
using HairScheduler.Exception;
using HairScheduler.Exception.ExceptionBase;

namespace HairScheduler.Application.UseCases.Schedules.Delete;

public class DeleteUseCase : IDeleteUseCase
{
    private readonly IDeleteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUseCase(IDeleteRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(string nickname, DateTime date)
    {
        var remove = await _repository.Delete(nickname, date);
        if (remove is false)
        {
            throw new NotFoundException(ResourceErrors.NICKNAME_NOT_FOUND);
        }

        await _unitOfWork.Commit(); 
    }
}
