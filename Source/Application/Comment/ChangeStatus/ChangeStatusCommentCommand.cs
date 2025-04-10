using Common.Application;
using Domain.CommentAgg;
using Domain.CommentAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comment.ChangeStatus
{
    public record ChangeStatusCommentCommand(long Id, CommentStatus Status) : IBaseCommand;
    internal class ChangeStatusCommentCommandHandler : IBaseCommandHandler<ChangeStatusCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public ChangeStatusCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeStatusCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetTracking(request.Id);
            if (comment == null)
                return OperationResult.NotFound();
            comment.ChangeStatus(request.Status);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
