using Common.Application;
using Common.Application.Validation;
using Domain.CommentAgg.Repository;
using FluentValidation;

namespace Application.Comment.Edit
{
    public record EditCommentCommand(long CommentId, string Text, long UserId) : IBaseCommand;
   
    public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public EditCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetTracking(request.CommentId);
            if (comment == null)
                return OperationResult.NotFound();
            comment.Edit(request.Text);
            return OperationResult.Success();
        }
    }
    public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(r => r.Text)
                .NotNull()
                .MinimumLength(2).WithMessage(ValidationMessages.minLength("متن نظر", 2));


        }
    }
}
