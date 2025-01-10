using Common.Application;

namespace Planning.Api.Model.InstagramModel
{
    public class InstagramViewModel
    {
        public class AddPostInstagramViewModel: IBaseCommand
        {
            public string InstagramId { get; set; } = string.Empty;
            public string DateOfPosting { get; set; }
            public string Link { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ImageName { get; set; } = string.Empty;
            public string VideoName { get; set; } = string.Empty;

        }

    }
}
