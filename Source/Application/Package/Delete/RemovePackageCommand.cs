using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.PackageAgg.Repository;

namespace Application.Package.Delete
{
    public record RemovePackageCommand(long id) : IBaseCommand;

    public class RemovePackageCommandHandler : IBaseCommandHandler<RemovePackageCommand>
    {
        private readonly IPackageRepository _repository;

        public RemovePackageCommandHandler(IPackageRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemovePackageCommand request, CancellationToken cancellationToken)
        {
            //var result = await _repository.Delete(request.id);
            var package = await _repository.GetTracking(request.id);

            if(package == null)
                return OperationResult.NotFound();
            else if(package.Active == true)
                return OperationResult.Error("امکان حذف یک پکیج فعال وجود نداره!");
            await _repository.Delete(package.Id);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
