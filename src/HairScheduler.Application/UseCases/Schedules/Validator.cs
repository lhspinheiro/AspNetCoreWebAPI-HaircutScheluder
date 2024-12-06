using FluentValidation;
using HairScheduler.Communication.Requests;
using HairScheduler.Exception;

namespace HairScheduler.Application.UseCases.Schedules;

public class Validator : AbstractValidator<RequestSchedule>
{
    public Validator()
    {
        RuleFor(schudule => schudule.Name).NotEmpty().WithMessage(ResourceErrors.NAME_REQUIRED);
        RuleFor(schudule => schudule.Nickname).NotEmpty().WithMessage(ResourceErrors.NICKNAME_REQUIRED);
        RuleFor(schudule => schudule.Date).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrors.DATE_PAST);
        RuleFor(schudule => schudule.haircutCategory).IsInEnum().WithMessage(ResourceErrors.CATEGORY_HAIRCUT_INVALID);
        RuleFor(schudule => schudule.paymentType).IsInEnum().WithMessage(ResourceErrors.PAYMENT_INVALID);
    }
}
