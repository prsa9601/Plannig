using Common.Application;

namespace Planning.Api.Model.Telegram
{
    public record class CreateTelegramAccountCommandViewModel(string? Token, string ChatId, bool UsedDefaultToken) : IBaseCommand;

}
