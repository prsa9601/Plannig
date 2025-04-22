using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Domain.SocialMediaAgg.InstagramAgg;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Domain.SocialMediaAgg.InstagramAgg.Service;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Instagram.Account.Add
{
    public class AddInstagramAccountCommand : IBaseCommand
    {
        //public string InstagramId { get; set; } //InstagramAccountId
        //public string PageId { get; set; } //PageId
        public string accessToken { get; set; } //AccessToken Instagram
        public string UserName { get; set; } //AccessToken Instagram
        public string UserId { get; set; }
        public IFormFile Profile { get; set; } //token Telegram
    }
    internal class AddInstagramAccountCommandHandler : IBaseCommandHandler<AddInstagramAccountCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;
        private readonly IInstagramService _service;

        public AddInstagramAccountCommandHandler(IInstagramRepository repository, IFileService fileService, IInstagramService service)
        {
            _repository = repository;
            _fileService = fileService;
            _service = service;
        }

        public async Task<OperationResult> Handle(AddInstagramAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var instagram = new Domain.SocialMediaAgg.InstagramAgg.Instagram
                          (request.accessToken, request.UserName, request.UserId, _service);

                if (request.Profile != null)
                {
                    string imageName = await _fileService.SaveFileAndGenerateName
                        (request.Profile, Directories.InstagramProfile);
                    instagram.ChangeImage(imageName);
                }
                _repository.Add(instagram);
                await _repository.Save();
                return OperationResult.Success();
            }
            catch (Exception e)
            {
                return OperationResult.Error(e.Message);
            }
        }
    }
    public class AddInstagramAccountCommandValidator : AbstractValidator<AddInstagramAccountCommand>
    {
        public AddInstagramAccountCommandValidator()
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
