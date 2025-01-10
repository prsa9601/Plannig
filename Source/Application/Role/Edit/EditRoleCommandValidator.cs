using Common.Application.Validation;
using FluentValidation;

namespace Application.Role.Edit
{
    public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
    {
        public EditRoleCommandValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("Title"));

        }
    }
}
