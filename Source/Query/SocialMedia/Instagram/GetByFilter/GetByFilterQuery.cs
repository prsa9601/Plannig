using Common.Query;
using Query.SocialMedia.Instagram.DTOs;

namespace Query.SocialMedia.Instagram.GetByFilter
{
    public class GetByFilterQuery : QueryFilter<InstagramFilterResult, InstagramFilterParam>
    {
        public GetByFilterQuery(InstagramFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetByFilterQueryHandler : IQueryHandler<GetByFilterQuery, InstagramFilterResult>
    {
        public Task<InstagramFilterResult> Handle(GetByFilterQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
