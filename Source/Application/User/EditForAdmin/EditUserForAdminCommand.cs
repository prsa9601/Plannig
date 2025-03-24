using Common.Application;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.EditForAdmin
{
    public class EditUserForAdminCommand : IBaseCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    internal class EditUserForAdminCommandHandler : IBaseCommandHandler<EditUserForAdminCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly IUserService _service;
        public EditUserForAdminCommandHandler(IUserRepository<Domain.UserAgg.User> repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditUserForAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.Id);
            if (user == null)
                return OperationResult.NotFound();
            user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email, request.userName, _service);

            user.ChangeActivityStatus(request.IsActive);
                await _repository.Save();
            return OperationResult.Success();
        }
    }
}
