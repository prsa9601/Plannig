using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.Account.DTOs;
using Query.SocialMedia.Telegram.DTOs;

namespace Query.SocialMedia.Instagram.Account.GetByFilter
{
    public class GetInstagramAccountByFilter : QueryFilter<InstagramAccountFilterResult, InstagramAccountFilterParam>
    {
        public GetInstagramAccountByFilter(InstagramAccountFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetInstagramAccountByFilterHandler : IQueryHandler<GetInstagramAccountByFilter, InstagramAccountFilterResult>
    {
        private readonly PlanningContext _context;

        public GetInstagramAccountByFilterHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<InstagramAccountFilterResult> Handle(GetInstagramAccountByFilter request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Instagram.Select(i => i);

            if (!string.IsNullOrEmpty(param.UserName))
                result = result.Where(i => i.UserName.Equals(param.UserName));

            if (!string.IsNullOrEmpty(param.InstagramUserName))
                result = result.Where(i => i.InstagramUserName.Equals(param.InstagramUserName));

            if (param.StartTime != null && param.StartTime != DateTime.MinValue)
                result = result.Where(i => i.CreationDate >= param.StartTime);

            if (param.EndTime != null && param.EndTime != DateTime.MaxValue)
                result = result.Where(i => i.CreationDate <= param.EndTime);
            //if (!string.IsNullOrEmpty(param.PhoneNumbeer))
            //    result=result.Where(i=>i.pho)
            //if(!string.IsNullOrEmpty(param.Title))
            //    result = result.Where(i=>i.)
            switch (param.SearchOrderBy)
            {
                case Instagram.Account.DTOs.PostInstagramAccountSearchOrderBy.latest:
                    {
                        result.OrderByDescending(i => i.CreationDate);
                        break;
                    }
            }
            var skip = (@param.PageId - 1) * @param.Take;
            var model = new InstagramAccountFilterResult()
            {
                Data = await result.Skip(skip).Take(@param.Take).Select(s => s.Map()!)
                    .ToListAsync(cancellationToken),
                FilterParams = @param
            };
            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
