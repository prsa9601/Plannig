using Application.Notification.Edit;
using Common.Application.Validation;
using FluentValidation;

internal class EditNotificationCommandValidator : AbstractValidator<EditNotificationCommand>
{
    public EditNotificationCommandValidator()
    {
        RuleFor(r => r.UserNames).NotEmpty().NotNull()
            .WithMessage(ValidationMessages.required("User"));

        //RuleFor(b => b.AllowedEmailCount).NotNull()
        //    .WithMessage(ValidationMessages.minLength("تعداد ایمیل های مجاز", 0));

        //RuleFor(r => r.AllowedEmailCount).NotNull()
        //    .WithMessage(ValidationMessages.minLength("تعداد پیامک های مجاز", 0));

    }
}

