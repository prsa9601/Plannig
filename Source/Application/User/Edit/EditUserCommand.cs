using Common.Application;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;

namespace Application.User.Edit
{
    public class EditUserCommand : IBaseCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly IUserService _service;

        public EditUserCommandHandler(IUserRepository<Domain.UserAgg.User> repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.Id);
            if (user == null) 
                return OperationResult.NotFound();
            user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email, request.userName, _service);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
