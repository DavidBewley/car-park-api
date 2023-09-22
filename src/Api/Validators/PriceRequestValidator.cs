using FluentValidation;
using Core.Models.Requests;

namespace Api.Validators
{
    public class PriceRequestValidator : AbstractValidator<AvailabilityRequest>
    {
        public PriceRequestValidator()
        {
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date must be specified");
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date must be specified");
            RuleFor(x => x.EndDate.Date)
                .GreaterThan(x => x.StartDate.Date)
                .WithMessage("End date must be after the start date");
        }
    }
}
