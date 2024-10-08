using Common.Application;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Logout
{
    public class LogoutUserCommand : IBaseCommand
    {
    }
    public class LogoutUserCommandHandler : IBaseCommandHandler<LogoutUserCommand>
    {
        private readonly SignInManager<Domain.UserAgg.User> _signInManager;

        public LogoutUserCommandHandler(SignInManager<Domain.UserAgg.User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<OperationResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _signInManager.SignOutAsync();
                return OperationResult.Success();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
