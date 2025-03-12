using Common.Application;
using Common.Application.Schedule;
using Common.Application.Validation;
using Domain.EventAgg.Enum;
using Domain.EventAgg.Repository;
using Domain.UserAgg.Repository;
using FluentValidation;

namespace Application.Event.Edit
{
    public class EditEventCommand : IBaseCommand<long>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string EventAddress { get; set; }
        public bool accessNotification { get; set; }
        public string creatorUserName{ get; set; }
        

        public List<string> userNames { get; set; }
        public Tagged tag { get; set; }
        public Domain.EventAgg.Enum.NotificationEnum NotificationEnum { get; set; }
    }
    public class EditEventCommandHandler : IBaseCommandHandler<EditEventCommand, long>
    {
        private readonly IEventRepository _repository;
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        private readonly EventScheduler _schedule;

        public EditEventCommandHandler(IEventRepository repository, EventScheduler schedule, IUserRepository<Domain.UserAgg.User> userRepository)
        {
            _repository = repository;
            _schedule = schedule;
            _userRepository = userRepository;
        }

        public async Task<OperationResult<long>> Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            var Event = await _repository.GetTracking(request.Id);
            if (Event == null)
                return OperationResult<long>.NotFound(0);
            var creator = await _userRepository.GetTrackingByUserName(request.creatorUserName);
            List<Domain.UserAgg.User>? users = new List<Domain.UserAgg.User>();
            List<string>? usersEmail = new List<string>();
            if (creator.friends.Count() > 0)
            {
                users = await _userRepository.GetListAsync(request.userNames);
                foreach (var item in users)
                {
                    usersEmail.Add(item.Email);
                }
            }
            //var oldEvent = Event;
            Event.Edit(request.creatorUserName, request.userNames,
                request.Title, request.StartTime, request.EndTime, 
                request.Description, request.Link, request.EventAddress,
                request.accessNotification, request.tag, request.NotificationEnum);
            //Event.AddUser(request.userNames);
            await _repository.Save();
            _schedule.UpdateEvent(Event.Id,request.StartTime, creator.Email, 
                request.Title,request.Description, usersEmail);
          
            return OperationResult<long>.Success(Event.Id);
        }
    }
    public class EditEventCommandValidator : AbstractValidator<EditEventCommand>
    {
        public EditEventCommandValidator()
        {
            RuleFor(r => r.StartTime)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("StartTime"));

            RuleFor(r => r.Title)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Title"));

            RuleFor(r => r.Description)
               .NotNull().NotEmpty()
               .WithMessage(ValidationMessages.required("Description"));

            RuleFor(r => r.EndTime)
               .NotNull().NotEmpty()
               .WithMessage(ValidationMessages.required("EndTime"));

            RuleFor(r => r.StartTime >= r.EndTime)
                .NotNull().When(r => r.StartTime >= r.EndTime).WithMessage("تاریخ شروع و پایان مصابقت ندارند!");

            //RuleFor(r => r.Link)
            //   .NotNull().NotEmpty()
            //   .WithMessage(ValidationMessages.required("Link"));

            //RuleFor(f => f.EventAddress)
            //    .NotNull().NotEmpty()
            //   .WithMessage(ValidationMessages.required("EventAddress"));
        }

    }
}
