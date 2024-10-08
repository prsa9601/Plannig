using Common.Application;

namespace Application.User.SetEvent
{
    public record class SetUserEventCommand(List<long> eventsId, string userId) : IBaseCommand;
}
