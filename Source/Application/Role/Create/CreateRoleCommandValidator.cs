using Common.Application.Validation;
using FluentValidation;

namespace Application.Role.Create
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("Title"));
        }
    }
}
