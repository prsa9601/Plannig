using Common.Application.Validation;
using FluentValidation;

namespace Application.Notification.Add
{
    internal class AddNotificationCommandValidator : AbstractValidator<AddNotificationCommand>
    {
        public AddNotificationCommandValidator()
        {
            RuleFor(r => r.UserNames).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("User"));

            //RuleFor(b => b.AllowedEmailCount).NotNull()
            //    .WithMessage(ValidationMessages.minLength("تعداد ایمیل های مجاز", 0));

            //RuleFor(r => r.AllowedEmailCount).NotNull()
            //    .WithMessage(ValidationMessages.minLength("تعداد پیامک های مجاز", 0));
            //RuleFor(r => r.Text)
            //    .NotNull()
            //    .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
        }
    }
}
