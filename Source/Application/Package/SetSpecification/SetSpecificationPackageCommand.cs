using Common.Application;
using Domain.PackageAgg;
using Domain.PackageAgg.Repository;

namespace Application.Package.SetSpecification
{
    public class SetSpecificationPackageCommand : IBaseCommand
    {
        public long id { get; set; }
        public Dictionary<string, string> Specifications { get; set; }
    }
    public class SetSpecificationPackageCommandHandler : IBaseCommandHandler<SetSpecificationPackageCommand>
    {
        private readonly IPackageRepository _repository;

        public SetSpecificationPackageCommandHandler(IPackageRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetSpecificationPackageCommand request, CancellationToken cancellationToken)
        {
            var package = await _repository.GetTracking(request.id);
            var specifications = new List<PackageSpecification>();
            request.Specifications.ToList().ForEach(specification =>
            {
                specifications.Add(new PackageSpecification(specification.Key, specification.Value));
            });

            package.SetSpecification(specifications);

            await _repository.Save();
            return OperationResult.Success();

        }
    }
}
