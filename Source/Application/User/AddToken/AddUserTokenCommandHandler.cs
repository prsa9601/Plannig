using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User.AddToken;

public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository<Domain.UserAgg.User> _repository;

    public AddUserTokenCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTrackingWithString(request.UserId);
        if (user == null)
            return OperationResult.NotFound();


        user.AddToken(request.HashJwtToken, request.HashRefreshToken, request.TokenExpireDate, request.RefreshTokenExpireDate, request.Device);
        await _repository.Save();
        return OperationResult.Success();
    }
}