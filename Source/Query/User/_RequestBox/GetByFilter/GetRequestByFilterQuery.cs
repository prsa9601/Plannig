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

            var requestBox = await _context.Users.Select(i => i.RequestBox).ToListAsync();

            var result =  requestBox.Select(i=>
                i.Where(i=>i.ReceiverId.Equals(request.FilterParams.UserId)
                                                        ||i.SenderId.Equals(request.FilterParams.UserId)));
          
            List<RequestBoxFilterData> requests1 = new List<RequestBoxFilterData>();

            foreach (var item in result) 
            {
                foreach (var item1 in item)
                {


                    var data2 = new RequestBoxFilterData
                    {
                        Id = item1.Id,
                        CreationDate = item1.CreationDate,
                        UserNameSender = item1.SenderId.GetUserNameByIdUser(_context),
                        UserNameReceived = item1.ReceiverId.GetUserNameByIdUser(_context),
                        ReceivedId = item1.ReceiverId,
                        SenderId = item1.SenderId,
                        Description = item1.Description,
                        Title = item1.Title
                    };
                    requests1.Add(data2);
                }
            }

            switch (@params.filter)
            {
                case filter.SendRequest:
                    {
                        requests1 = requests1.Where(i=>i.SenderId == request.FilterParams.UserId).ToList();
                        if (requests1.Count == 0)
                            return null;
                        break;
                    }
                case filter.ReceiveRequest:
                    {
                        requests1 = requests1.Where(r => r.ReceivedId == request.FilterParams.UserId).ToList();
                        if (requests1.Count == 0)
                            return null;
                        break;
                    }
             

            }

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


