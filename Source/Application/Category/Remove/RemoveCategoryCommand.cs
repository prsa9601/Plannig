using Common.Application;
using Domain.CategoryAgg.Repository;

namespace Application.Category.Remove
{
    public record class RemoveCategoryCommand(long CategoryId) : IBaseCommand;
    internal class RemoveCategoryCommandHandler : IBaseCommandHandler<RemoveCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        public RemoveCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteOneEntity(i => i.Id.Equals(request.CategoryId));
            if (!result)
                return OperationResult.Error("خطایی در حذف دسته بندی رخ داده است!");
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
