using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace Application.User.RemoveToken
{
    public record RemoveUserTokenCommand(string UserId, long TokenId) : IBaseCommand<string>;

}
