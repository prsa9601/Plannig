using Common.Query;
using Query.SocialMedia.Telegram.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.SocialMedia.Telegram.Account.GetById
{
    public class GetTelegramAccountByIdQuery : IQuery<TelegramDto?>
    {
    }
    public class GetTelegramAccountByIdQueryHandler 
        : IQueryHandler<GetTelegramAccountByIdQuery, TelegramDto?>
    {
        public Task<TelegramDto?> Handle(GetTelegramAccountByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
