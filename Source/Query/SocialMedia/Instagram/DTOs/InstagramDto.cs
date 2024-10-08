using Common.Query;
using Domain.SocialMediaAgg.InstagramAgg;

namespace Query.SocialMedia.Instagram.DTOs
{
    public class InstagramDto : BaseDto
    {
        //Instagram
        public string accessToken { get; set; } //AccessToken Instagram
        public string UserName { get; set; }
        public string InstagramName { get; set; }
        public SendMethodInstagram SendMethod { get; set; } 

    }
  
}
