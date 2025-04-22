using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;

namespace Application.SocialMedia.Instagram.Story.Delete
{
    public class DeleteStoryCommand : IBaseCommand
    {
        public long StoryId { get; set; } //TableId
        public long InstagramId { get; set; }//TableId
    }
    internal class DeleteStoryCommandHandler : IBaseCommandHandler<DeleteStoryCommand>
    {
        private readonly IInstagramRepository _repository;

        public DeleteStoryCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteStoryCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTracking(request.InstagramId);
            if (instagram == null) 
                return OperationResult.NotFound();
            instagram.RemoveStory(request.StoryId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
