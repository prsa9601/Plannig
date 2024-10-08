using Common.Application;
using Domain.UserAgg;
using Domain.UserAgg.Repository;

namespace Application.User.Delete
{
    public class DeleteUserCommand : IBaseCommand
    {
        public string Id { get; set; }
    }
    public class DeleteUserCommandHandler : IBaseCommandHandler<DeleteUserCommand>
    {
        private readonly Domain.UserAgg.Repository.IUserRepository<Domain.UserAgg.User> _repository;

        public DeleteUserCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //var user = await _repository.GetTracking(request.Id);
            bool result = await _repository.Delete(request.Id);
            if (!result)
                return OperationResult.Error("مشکلی در حذف پیش آمده!");

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
