using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Telegram.DTOs;

namespace Query.SocialMedia.Telegram.GetById
{
    public record class GetTelegramByIdQuery(long id) : IQuery<TelegramDto?>;
 
    internal class GetTelegramByIdQueryHandler : IQueryHandler<GetTelegramByIdQuery, TelegramDto?>
    {
        private readonly PlanningContext _context;

        public GetTelegramByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<TelegramDto?> Handle(GetTelegramByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Telegrams.FirstOrDefaultAsync(i => i.Id == request.id);
            return result.MapTelegram();
        }
    }
}
