using Core.Models.Requests;
using FluentValidation;

namespace Api.Validators
{
    public class CreateBookingValidator : AbstractValidator<BookingRequest>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required");
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is required")
                .GreaterThanOrEqualTo(x=>x.StartDate.Date)
                .WithMessage("End date must be after start date");
        }
    }
}
