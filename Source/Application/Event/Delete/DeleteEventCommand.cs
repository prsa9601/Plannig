using Common.Application;
using Domain.EventAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Event.Delete
{
    public class DeleteEventCommand : IBaseCommand
    {
        public long Id { get; set; }
    }
    public class DeleteEventCommandHandler : IBaseCommandHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _repository;

        public DeleteEventCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            bool result = await _repository.Delete(request.Id);
            if (!result)
                return OperationResult.Error("مشکلی در حذف پیش آمده!");
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
