using Common.Application.Validation;
using FluentValidation;

namespace Application.Event.Add
{
    public class AddEventCommandValidator : AbstractValidator<AddEventCommand> 
    {
        public AddEventCommandValidator()
        {
            RuleFor(r => r.StartTime)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("StartTime"));

            RuleFor(r => r.Title)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Title"));

            RuleFor(r => r.Description)
               .NotNull().NotEmpty()
               .WithMessage(ValidationMessages.required("Description"));

            RuleFor(r => r.EndTime)
               .NotNull().NotEmpty()
               .WithMessage(ValidationMessages.required("EndTime"));
          
            RuleFor(r => DateTime.Parse(r.StartTime) >= DateTime.Parse(r.EndTime))
                .NotNull().When(r=> DateTime.Parse(r.StartTime) >= DateTime.Parse(r.EndTime)).WithMessage("تاریخ شروع و پایان مصابقت ندارند!");

            //RuleFor(r => r.Link)
            //   .NotNull().NotEmpty()
            //   .WithMessage(ValidationMessages.required("Link"));

            //RuleFor(f => f.EventAddress)
            //    .NotNull().NotEmpty()
            //   .WithMessage(ValidationMessages.required("EventAddress"));
        }

    }
}
