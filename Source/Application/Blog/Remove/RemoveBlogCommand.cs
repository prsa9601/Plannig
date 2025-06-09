using Common.Application;
using Domain.BlogAgg.Repository;

namespace Application.Blog.Remove
{
    public class RemoveBlogCommand : IBaseCommand
    {
        public long BlogId { get; set; }
    }
    internal class RemoveBlogCommandHandler : IBaseCommandHandler<RemoveBlogCommand>
    {
        private readonly IBlogRepository _repository;

        public RemoveBlogCommandHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.DeleteOneEntity(i => i.Id.Equals(request.BlogId));
            if (!blog)
                return OperationResult.Error("مشکلی در حذف بلاگ هست!");
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
