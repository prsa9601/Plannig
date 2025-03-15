using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Domain.PackageAgg;
using Domain.PackageAgg.Repository;
using Domain.PackageAgg.Service;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using static Domain.PackageAgg.Package;

namespace Application.Package.Add
{
    public class AddPackageCommand : IBaseCommand
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public ExpiryTime ExpiryTime { get; set; }
        public int AllowedEmailCount { get; set; }
        public int AllowedSmsCount { get; set; }
        public IFormFile Picture { get; set; }
        public Dictionary<string, string> Specifications { get; set; }

    }
    public class AddPackageCommandHandler : IBaseCommandHandler<AddPackageCommand>
    {
        private readonly IFileService _fileService;
        private readonly IPackageRepository _repository;
        private readonly IPackageService _service;

        public AddPackageCommandHandler(IFileService fileService, IPackageRepository repository, IPackageService service)
        {
            _fileService = fileService;
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(AddPackageCommand request, CancellationToken cancellationToken)
        {
            var imageName = await _fileService
                .SaveFileAndGenerateName(request.Picture, Directories.PackageImages);

            var package = new Domain.PackageAgg.Package(request.ExpiryTime, request.AllowedSmsCount, 
                request.AllowedEmailCount, request.Price, request.Title, imageName, request.Link, _service); 
            _repository.Add(package);
            //_repository.SaveChange();
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
    public class AddPackageCommandValidator : AbstractValidator<AddPackageCommand>
    {
        public AddPackageCommandValidator()
        {
            RuleFor(r => r.Picture)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Picture"));

            RuleFor(r => r.Title)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Picture"));

        }


    }
}
