using Common.Application;
using Common.Application.Validation;
using Domain.EventAgg.Enum;
using Domain.EventAgg.Repository;
using FluentValidation;

namespace Application.Event.Edit
{
    public class EditEventCommand : IBaseCommand
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
        public Domain.EventAgg.Enum.Notification notification { get; set; }
    }
    public class EditEventCommandHandler : IBaseCommandHandler<EditEventCommand>
    {
        private readonly IEventRepository _repository;

        public EditEventCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            var Event = await _repository.GetTracking(request.Id);
            if (Event == null)
                return OperationResult.NotFound();

            Event.Edit(request.creatorUserName, request.userNames, request.Title, request.StartTime, request.EndTime, request.Description, request.Link, request.EventAddress, request.accessNotification, request.tag, request.notification);
            //Event.AddUser(request.userNames);

            await _repository.Save();
            return OperationResult.Success();
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
