using Common.Application;
using MediatR;
using Application.Role.Create;
using Application.Role.Delete;
using Application.Role.Edit;
using Query.Role.DTOs;
using Query.Role.GetByFilter;
using Query.Role.GetById;
using Query.Role.GetList;

namespace Presentation.Facade.Role
{
    public class RoleFacade : IRoleFacade
    {
        private readonly IMediator _mediator;

        public RoleFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateRole(CreateRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditRole(EditRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<RoleDto?> GetRoleById(string roleId)
        {
            return await _mediator.Send(new GetRoleByIdQuery(roleId));
        }

        public async Task<RoleFilterResult?> GetRolesByFilter(RoleFilterParam param)
        {
            return await _mediator.Send(new GetRoleByFilterQuery(param));
        }
        public async Task<List<RoleDto>?> GetRoles()
        {
            return await _mediator.Send(new GetRoleListQuery());
        }

        public async Task<OperationResult> DeleteRole(DeleteRoleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
