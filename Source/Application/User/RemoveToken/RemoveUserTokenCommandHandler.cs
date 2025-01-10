using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User.RemoveToken
{
    public class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand, string>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;

        public RemoveUserTokenCommandHandler(IUserRepository<Domain.UserAgg.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<string>> Handle(RemoveUserTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetTrackingWithString(request.UserId);
            if (user == null)
                return OperationResult<string>.NotFound();

            var token = user.RemoveToken(request.TokenId);
            await _userRepository.Save();
            return OperationResult<string>.Success(token);
        }
    }
}
