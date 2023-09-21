using FluentValidation;
using System;
using Core.Models.Requests;

namespace Api.Validators
{
    public class PriceRequestValidator : AbstractValidator<PriceRequest>
    {
        public PriceRequestValidator()
        {
            RuleFor(x => x.EndDate.Date).GreaterThan(x => x.StartDate.Date)
                .WithMessage("End date must be after the start date");
        }
    }
}
