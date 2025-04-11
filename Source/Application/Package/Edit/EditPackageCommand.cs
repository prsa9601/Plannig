using Application._Utilities;
using Application.Package.Add;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Domain.PackageAgg;
using Domain.PackageAgg.Repository;
using Domain.PackageAgg.Service;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using static Domain.PackageAgg.Package;

namespace Application.Package.Edit
{
    public class EditPackageCommand : IBaseCommand
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public long Id { get; set; }
        public ExpiryTime ExpiryTime { get; set; }
        public int AllowedEmailCount { get; set; }
        public int AllowedSmsCount { get; set; }
        public int AllowedPostTelegram { get; set; }
        public int AllowedPostInstagram { get; set; }
        public int AllowedStoryInstagram { get; set; }
        public IFormFile? Picture { get; set; }
        public Dictionary<string, string> Specifications { get; set; }

    }
    internal class EditPackageCommandHandler : IBaseCommandHandler<EditPackageCommand>
    {
        private readonly IFileService _fileService;
        private readonly IPackageRepository _repository;
        private readonly IPackageService _service;

        public EditPackageCommandHandler(IFileService fileService, IPackageRepository repository, IPackageService service)
        {
            _fileService = fileService;
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditPackageCommand request, CancellationToken cancellationToken)
        {
            //addimage
            //var imageName = await _fileService
            //    .SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);



            var package = await _repository.GetTracking(request.Id);
            if (package == null)
                return OperationResult.NotFound();
            var oldAvatar = package.ImageName;
            if (request.Picture != null)
            {
                var imageName = await _fileService
                    .SaveFileAndGenerateName(request.Picture, Directories.PackageImages);
                package.SetImage(imageName);

                DeleteOldImage(request.Picture, oldAvatar);

            }

            package.Edit(request.ExpiryTime, request.AllowedSmsCount,
                request.AllowedEmailCount, request.Price, request.Title,
                request.Link, request.AllowedPostTelegram, request.AllowedPostInstagram,
                request.AllowedStoryInstagram, _service);

            var specifications = new List<PackageSpecification>();
            request.Specifications.ToList().ForEach(specification =>
            {
                specifications.Add(new PackageSpecification(specification.Key, specification.Value));
            });

            package.SetSpecification(specifications);
            await _repository.Save();

            return OperationResult.Success();
        }
        private void DeleteOldImage(IFormFile? avatarFile, string oldImage)
        {
            if (avatarFile == null || oldImage == "avatar.png")
                return;

            _fileService.DeleteFile(Directories.UserAvatars, oldImage);
        }
        public class EditPackageCommandValidator : AbstractValidator<EditPackageCommand>
        {
            public EditPackageCommandValidator()
            {
                RuleFor(r => r.Picture)
                    .NotNull().NotEmpty()
                    .WithMessage(ValidationMessages.required("Picture"));

                RuleFor(r => r.Title)
                    .NotNull().NotEmpty()
                    .WithMessage(ValidationMessages.required("Title"));


                RuleFor(r => r.AllowedEmailCount)
                    .NotNull().NotEmpty()
                    .GreaterThanOrEqualTo(0) // چک می‌کند که مقدار کمتر از صفر نباشد
                    .WithMessage(ValidationMessages.required("AllowedEmailCount باید بزرگتر یا مساوی صفر باشد"));

                RuleFor(r => r.AllowedSmsCount)
                    .NotNull().NotEmpty()
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("AllowedSmsCount باید بزرگتر یا مساوی صفر باشد");

                RuleFor(r => r.AllowedPostTelegram)
                    .NotNull().NotEmpty()
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("AllowedPostTelegram باید بزرگتر یا مساوی صفر باشد");

                RuleFor(r => r.AllowedPostInstagram)
                    .NotNull().NotEmpty()
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("AllowedPostInstagram باید بزرگتر یا مساوی صفر باشد");

                RuleFor(r => r.AllowedStoryInstagram)
                    .NotNull().NotEmpty()
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("AllowedStoryInstagram باید بزرگتر یا مساوی صفر باشد");
            }


        }
    }
}