using Common.Application;
using Domain.UserAgg.Repository;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Application.User.ChangeActivityUserStatus
{
    public class ChangeActivityUserStatusCommand : IBaseCommand
    {
        public required string UserId { get; set; }
        public bool IsActive { get; set; }
    }
    internal class ChangeActivityUserStatusCommandHandler : IBaseCommandHandler<ChangeActivityUserStatusCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public ChangeActivityUserStatusCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeActivityUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.Id == request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.ChangeActivityStatus(request.IsActive);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
