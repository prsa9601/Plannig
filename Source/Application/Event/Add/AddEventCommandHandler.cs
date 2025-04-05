using Common.Application;
using Common.Application.Schedule;
using Common.Application.SecurityUtil;
using Domain.EventAgg.Enum;
using Domain.EventAgg.Repository;
using Domain.EventAgg.Service;
using Domain.UserAgg.Repository;

namespace Application.Event.Add
{
    public class AddEventCommandHandler : IBaseCommandHandler<AddEventCommand,long>
    {
        private readonly IEventRepository _repository;
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        private readonly IEventService _service;
        private readonly EventScheduler _schedule;

        public AddEventCommandHandler(IEventRepository repository, IEventService service, EventScheduler schedule, IUserRepository<Domain.UserAgg.User> userRepository)
        {
            _repository = repository;
            _service = service;
            _schedule = schedule;
            _userRepository = userRepository;
        }

        public async Task<OperationResult<long>> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var Event = new Domain.EventAgg.Event(request.creatorUserName, request.userNames,
                request.Title, request.StartTime, request.EndTime, request.Description,
                request.Link, request.EventAddress, request.tag, request.NotificationEnum,
                request.accessNotification);

            await _repository.AddAsync(Event);
            // await _service.Schedule("411f8274-5ee7-4bcc-8d43-e5214aa79aa7","aaaaaaaaaaa",DateTime.Now.AddSeconds(5), cancellationToken);
            await _repository.Save();
            if (request.userNames.Count() == 0)
            {
                Event.AddEventUser(request.creatorUserName);
            }
            else
            {
                Event.AddEventUser(request.creatorUserName, request.userNames);
            }
            var creator = await _userRepository.GetTrackingByUserName(request.creatorUserName);
            List<Domain.UserAgg.User>? users = new List<Domain.UserAgg.User>();
            List<string>? usersEmail = new List<string>();
            if (creator.friends.Count() > 0)
            {
                users.AddRange(await _userRepository.GetListAsync(request.userNames));
                if (users.Count() == 0 || users == null)
                {
                    foreach (var item in users)
                    {
                        usersEmail.Add(item.UserName);
                    }
                }
            }

           await _repository.Save();
     

            return OperationResult<long>.Success(Event.Id);
        }
    }
}
     //var Event = new Domain.EventAgg.Event(request.userNames, request.Title, DateTime.Parse(request.StartTime), DateTime.Parse(request.EndTime),request.Description, request.Link, request.EventAddress, request.tag,request.NotificationEnum,request.accessNotification);
            //_repository.Add(Event);
            // await _service.Schedule("411f8274-5ee7-4bcc-8d43-e5214aa79aa7","aaaaaaaaaaa",DateTime.Now.AddSeconds(5), cancellationToken);
        
//var Event2 = await _repository.FindEvent(request.Title, request.StartTime, request.EndTime, request.Description, request.Link, request.EventAddress, request.tag, request.NotificationEnum, request.accessNotification);
            //Event.AddUser(request.userNames);
            // await _repository.Save();
            //if (request.accessNotification == true)
            //{
            //    var q = _schedule.ScheduleEvent(Event.StartTime,
            //        creator.Email, Event.Id, Event.Description, Event.Title,
            //        usersEmail);
            //}