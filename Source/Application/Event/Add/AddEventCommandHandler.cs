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
            var Event = new Domain.EventAgg.Event(request.userNumber, request.Title,request.StartTime, request.EndTime,request.Description, request.Link, request.EventAddress, request.tag,request.notification,request.accessNotification);
            _repository.Add(Event);
            await _repository.Save();
            //var Event2 = await _repository.FindEvent(request.Title, request.StartTime, request.EndTime, request.Description, request.Link, request.EventAddress, request.tag, request.notification, request.accessNotification);
            //Event.AddUser(request.userNumber);
           // await _repository.Save();
            return OperationResult.Success();
        }
    }
}
