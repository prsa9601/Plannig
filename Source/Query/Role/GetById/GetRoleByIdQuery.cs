using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Role.DTOs;
using Query.Role.GetById;

namespace Query.Role.GetById
{
    public record class GetRoleByIdQuery(string roleId) : IQuery<RoleDto?>;

}

public class GetQueryByIdHandler : IQueryHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly PlanningContext _context;

    public GetQueryByIdHandler(PlanningContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.roleId);
        if (role == null)
            return null;
        return new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
            Name = role.Name
        };
    }
}