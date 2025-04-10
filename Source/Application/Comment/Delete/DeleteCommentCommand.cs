using Common.Application;
using Domain.CommentAgg.Repository;

namespace Application.Comment.Remove
{
    public record class DeleteCommentCommand(long commentId) : IBaseCommand;
    public class DeleteCommentCommandHandler : IBaseCommandHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _repository;
        public DeleteCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteComment(request.commentId);
            if (!result)
                return OperationResult.Error();
            return OperationResult.Success();
        }
    }
}
