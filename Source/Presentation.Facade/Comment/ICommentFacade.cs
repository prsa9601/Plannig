using Application.Comment.ChangeStatus;
using Application.Comment.Create;
using Application.Comment.Delete;
using Application.Comment.Edit;
using Common.Application;
using MediatR;
using Query.Comment.DTOs;
using Query.Comment.GetByFilter;
using Query.Comment.GetById;

namespace Presentation.Facade.Comment
{
    public interface ICommentFacade
    {
        Task<OperationResult> Create(CreateCommentCommand command);
        Task<OperationResult> Edit(EditCommentCommand command);
        Task<OperationResult> Remove(DeleteCommentCommand command);
        Task<OperationResult> ChangeStatus(ChangeStatusCommentCommand command);
        Task<CommentDto?> GetCommentById(long CommentId);
        Task<CommentFilterResult?> GetCommentByFilter(CommentFilterParam param);
    }
    internal class CommentFacade : ICommentFacade
    {
        private readonly IMediator _mediator;

        public CommentFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> ChangeStatus(ChangeStatusCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Create(CreateCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommentFilterResult?> GetCommentByFilter(CommentFilterParam param)
        {
            return await _mediator.Send(new GetCommentByFilterQuery(param));
        }

        public async Task<CommentDto?> GetCommentById(long CommentId)
        {
            return await _mediator.Send(new GetCommentByIdQuery
            {
                CommentId = CommentId   
            });
        }

        public async Task<OperationResult> Remove(DeleteCommentCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
