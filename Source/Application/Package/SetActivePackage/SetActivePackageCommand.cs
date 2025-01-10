using System.Collections;
using Microsoft.EntityFrameworkCore;
using Common.Application;
using Domain.PackageAgg;
using Domain.PackageAgg.Repository;
using Domain.PackageAgg.Service;

namespace Application.Package.SetActivePackage
{
    public class SetActivePackageCommand : IBaseCommand
    {
        public required long Id { get; set; }
    }
    internal class SetActivePackageCommandHandler : IBaseCommandHandler<SetActivePackageCommand>
    {
        private readonly IPackageRepository _repository;
        private readonly IPackageService _service;

        public SetActivePackageCommandHandler(IPackageRepository repository, IPackageService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(SetActivePackageCommand request, CancellationToken cancellationToken)
        {
            var package = await _repository.GetTracking(request.Id);
            if (package == null)
                return OperationResult.NotFound();
            var packages = await _repository.GetListTrackingAsync();
            int activePackageCount = 0;
            foreach (var item in packages)
            {
                if (item.Active == true)
                {
                    activePackageCount += 1;
                }
            }
            if (activePackageCount < 3)
            {
                // await Context.SaveChangesAsync();
                package.SetActivePackage();
                await _repository.Save();
                return OperationResult.Success();
            }
            return OperationResult.Error("امکان فعال کردن چهار تا پکیج به صورت همزمان وجود ندارد!");
        }
    }
}