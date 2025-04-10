using Common.Application;
using Domain.BlogAgg.Repository;

namespace Application.Blog.IncreaseVisit
{
    public class IncreaseBlogVisitCommand : IBaseCommand
    {
        public long BlogId { get; set; }
    }
    internal class IncreaseBlogVisitCommandHandler : IBaseCommandHandler<IncreaseBlogVisitCommand>
    {
        private readonly IBlogRepository _repository;

        public IncreaseBlogVisitCommandHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(IncreaseBlogVisitCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetTracking(request.BlogId);
            if (blog == null)
                return OperationResult.NotFound();
            blog.IncreaseVisit();
            return OperationResult.Success();
        }
    }
}
