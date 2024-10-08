 using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User.SetEvent
{
    public class SetUserEventCommandHandler : IBaseCommandHandler<SetUserEventCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public SetUserEventCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetUserEventCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.userId);
            if (user == null)
                return OperationResult.NotFound();
        
            user.AddEvent(request.eventsId);
            await _repository.Save();
            return OperationResult.Success(); 
        }
    }
}
