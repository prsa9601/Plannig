using Common.Domain;

namespace Domain.SocialMediaAgg.InstagramAgg
{
    public class InstagramProfile : BaseEntity
    {
        public string TelegramId { get; set; }
        public string ImageName { get; set; }

        public InstagramProfile(string imageName)
        {
            ImageName = imageName;
        }
    }
}
