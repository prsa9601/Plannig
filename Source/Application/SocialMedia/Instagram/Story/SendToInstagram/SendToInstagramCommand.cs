using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;

namespace Application.SocialMedia.Instagram.Story.SendToInstagram
{
    public class SendToInstagramCommand : IBaseCommand
    {
        public string InstagramId { get; set; }
        public string AccessToken { get; set; }
        public string ImagePath { get; set; }
        public string Token { get; set; }
    }
    internal class SendToInstagramCommandHandler : IBaseCommandHandler<SendToInstagramCommand>
    {
        private readonly IInstagramRepository _repository;

        public SendToInstagramCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SendToInstagramCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.UploadStory(
               request.InstagramId, request.AccessToken, request.ImagePath, request.Token);
            return OperationResult.Success();
        }
    }
}
