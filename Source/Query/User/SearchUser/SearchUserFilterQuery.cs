using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;

namespace Query.User.SearchUser
{
    public class SearchUserFilterQuery : QueryFilter<UserFilterResult, UserFilterParam>
    {
        public SearchUserFilterQuery(UserFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class SearchUserQueryFilterHandler : IQueryHandler<SearchUserFilterQuery, UserFilterResult>
    {
        private readonly PlanningContext _context;

        public SearchUserQueryFilterHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserFilterResult> Handle(SearchUserFilterQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Users.Select(i=>i).AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(@param.UserName))
            {
                result = result.Where(i => i.UserName.Contains(@param.UserName));
            }

            //if (!string.IsNullOrWhiteSpace(@param.CurrentUserId))
            //{
            //    result = result.Where(i => i.UserName.Contains(@param.UserName));
            //}

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UserFilterResult()
            {
                Data = await result.Skip(skip).Take(@param.Take).Select(s => s.Map(param.CurrentUserId,_context))
                    .ToListAsync(cancellationToken),
                FilterParams = @param
            };
            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
