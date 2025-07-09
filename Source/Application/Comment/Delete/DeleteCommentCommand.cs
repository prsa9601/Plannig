using Common.Application;
using Domain.CommentAgg.Repository;

namespace Application.Comment.Delete
{
    public record class DeleteCommentCommand(long CommentId) : IBaseCommand;
    public class DeleteCommentCommandHandler : IBaseCommandHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _repository;
        public DeleteCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteComment(request.CommentId);
            if (!result)
                return OperationResult.Error();
            return OperationResult.Success();
        }
    }
}
