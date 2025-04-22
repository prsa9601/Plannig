using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;

namespace Application.SocialMedia.Instagram.Account.Delete
{
    public class DeleteInstagramAccountCommand : IBaseCommand
    {
        public required long Id { get; set; }// TableId
    }
    internal class DeleteInstagramAccountCommandHandler : IBaseCommandHandler<DeleteInstagramAccountCommand>
    {
        private readonly IInstagramRepository _repository;

        public DeleteInstagramAccountCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteInstagramAccountCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteOneEntity(i=>i.Id.Equals(request.Id));
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
