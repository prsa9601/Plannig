using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._RequestBox.DTOs;

namespace Query.User._RequestBox.GetByFilter
{
    public class GetRequestByFilterQuery : QueryFilter<RequestBoxFilterResult, RequestBoxFilterParam>
    {
       
        public GetRequestByFilterQuery(RequestBoxFilterParam filterParams) : base(filterParams)
        {

            
        }
    }
    public class GetRequestByFilterQueryHandler : IQueryHandler<GetRequestByFilterQuery, RequestBoxFilterResult>
    {
        private readonly PlanningContext _context;

        public GetRequestByFilterQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<RequestBoxFilterResult> Handle(GetRequestByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;

            var user = await _context.Users.Where(i => i.UserName.Equals(request.FilterParams.UserName))
                .FirstOrDefaultAsync();

            var result = user.RequestBox;

            List<RequestBoxFilterData> requests1 = new List<RequestBoxFilterData>();

            foreach (var item in result) 
            {
                //foreach (var data in item) 
                //{
                    var data2 = new RequestBoxFilterData
                    {
                        Id = item.Id,
                        CreationDate = item.CreationDate,
                        UserNameSender = item.SenderId.GetUserNameByIdUser(_context),
                        UserNameReceived = item.ReceiverId.GetUserNameByIdUser(_context),
                    };
                    requests1.Add(data2); 
                //}
            }
            //if (@params.PostId != null)
            //    result = result.Where(r => r.PostId == @params.PostId);

            switch (@params.filter)
            {
                case filter.SendRequest:
                    {
                        requests1 = requests1.Where(i=>i.UserNameSender == user.UserName).ToList();
                        if (requests1.Count == 0)
                            return null;
                        break;
                    }
                case filter.ReceiveRequest:
                    {
                        requests1 = requests1.Where(r => r.UserNameReceived == user.UserName).ToList();
                        if (requests1.Count == 0)
                            return null;
                        break;
                    }
             

            }


            //if (@params.UserId != null)
            //    result = result.Where(r => r.UserId == @params.UserId);

            //if (@params.StartDate != null)
            //    result = result.Where(r => r.CreationDate.Date >= @params.StartDate.Value.Date);

            //if (@params.EndDate != null)
            //    result = result.Where(r => r.CreationDate.Date <= @params.EndDate.Value.Date);

             

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new RequestBoxFilterResult()
            {
                Data = requests1.Skip(skip).Take(@params.Take)
                    .Select(i => i)
                    .ToList(),
                FilterParams = @params
            };
            model.GeneratePaging(requests1.AsQueryable(), @params.Take, @params.PageId);
            return model;
        }
    }
}
//var model = new RequestBoxFilterResult()
//{
//    Data = await requests1.Skip(skip).Take(@params.Take)
//        .Select(i => i).AsQueryable()
//        .ToListAsync(cancellationToken),
//    FilterParams = @params
//};


