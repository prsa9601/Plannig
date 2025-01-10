using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.PackageAgg.Repository;
using Domain.PackageAgg.Service;

namespace Application.Package.RemoveActivePackage
{
    public class RemoveActivePackageCommand : IBaseCommand
    {
        public required long Id { get; set; }
    }
    internal class RemoveActivePackageCommandHandler : IBaseCommandHandler<RemoveActivePackageCommand>
    {
        private readonly IPackageRepository _repository;

        public RemoveActivePackageCommandHandler(IPackageRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveActivePackageCommand request, CancellationToken cancellationToken)
        {
            var package = await _repository.GetTracking(request.Id);
            if(package == null)
                return OperationResult.NotFound();

            package.RemoveActivePackage();
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
