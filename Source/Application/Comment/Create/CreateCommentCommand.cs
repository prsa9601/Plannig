using Common.Application;
using Common.Application.Validation;
using Domain.CommentAgg.Repository;
using FluentValidation;

namespace Application.Comment.Create
{
    public record CreateCommentCommand(string Text, long UserId, long ProductId) : IBaseCommand;
    internal class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public CreateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Domain.CommentAgg.Comment(request.UserId, request.ProductId, request.Text);
            _repository.Add(comment);
            await _repository.Save();
            return OperationResult.Success();
        }
        public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
        {
            public CreateCommentValidator()
            {
                RuleFor(r => r.Text)
                    .NotNull()
                    .MinimumLength(2).WithMessage(ValidationMessages.minLength("متن نظر", 2));
            }
        }
    }
}
