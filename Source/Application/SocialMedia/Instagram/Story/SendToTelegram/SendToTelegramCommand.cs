using Common.Application;

namespace Application.SocialMedia.Instagram.Story.SendToTelegram
{
    public class SendToTelegramCommand : IBaseCommand
    {
    }
    internal class SendToTelegramCommandHandler : IBaseCommandHandler<SendToTelegramCommand>
    {
        public Task<OperationResult> Handle(SendToTelegramCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
