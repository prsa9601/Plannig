using Common.Application;
using Domain.EventAgg.Repository;

namespace Application.Event.Add
{
    public class AddEventCommandHandler : IBaseCommandHandler<AddEventCommand>
    {
        private readonly IEventRepository _repository;

        public AddEventCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var Event = new Domain.EventAgg.Event(request.creatorUserName, request.userNames, request.Title, request.StartTime, request.EndTime,request.Description, request.Link, request.EventAddress, request.tag,request.notification,request.accessNotification);
            //var Event = new Domain.EventAgg.Event(request.userNames, request.Title, DateTime.Parse(request.StartTime), DateTime.Parse(request.EndTime),request.Description, request.Link, request.EventAddress, request.tag,request.notification,request.accessNotification);
            _repository.Add(Event);
            await _repository.Save();
            //var Event2 = await _repository.FindEvent(request.Title, request.StartTime, request.EndTime, request.Description, request.Link, request.EventAddress, request.tag, request.notification, request.accessNotification);
            //Event.AddUser(request.userNames);
           // await _repository.Save();
            return OperationResult.Success();
        }
    }
}
