using Application._Utilities;
using Application.SocialMedia.Instagram.Account.Add;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Domain.SocialMediaAgg.InstagramAgg.Service;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Instagram.Account.Edit
{
    public class EditInstagramAccountCommand : IBaseCommand
    {
        public required long Id { get; set; }
        public string accessToken { get; set; } //AccessToken Instagram
        public string UserName { get; set; } //AccessToken Instagram
        public string UserId { get; set; }
        public IFormFile? Profile { get; set; } //token Telegram
    }
    internal class EditInstagramAccountCommandHandler : IBaseCommandHandler<EditInstagramAccountCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;
        private readonly IInstagramService _service;
        public EditInstagramAccountCommandHandler(IInstagramRepository repository, IFileService fileService, IInstagramService service)
        {
            _repository = repository;
            _fileService = fileService;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditInstagramAccountCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTracking(request.Id);
            if (instagram == null)
                return OperationResult.NotFound();
            instagram.Edit(request.accessToken,request.UserName, request.UserId, _service);
            if (request.Profile != null)
            {
                var imageName = await _fileService.SaveFileAndGenerateName
                    (request.Profile, Directories.InstagramProfile);
                _fileService.DeleteFile(Directories.InstagramProfile, instagram.Profile);
                instagram.ChangeImage(imageName);
            }
            await _repository.Save();
            return OperationResult.Success();
        }

    }
    public class EditInstagramAccountCommandValidator : AbstractValidator<EditInstagramAccountCommand>
    {
        public EditInstagramAccountCommandValidator()
        {
            RuleFor(r => r.UserId)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(r => r.UserName)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("UserName"));
        }
    }
}
