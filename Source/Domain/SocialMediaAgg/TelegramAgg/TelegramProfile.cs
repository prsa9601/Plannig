using Common.Domain;

namespace Domain.SocialMediaAgg.TelegramAgg
{
    public class TelegramProfile : BaseEntity
    {
        public string TelegramId { get; set; }
        public string ImageName { get; set; }

        public TelegramProfile(string imageName)
        {
            ImageName = imageName;
        }
    }
}
