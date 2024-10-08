using Domain.SocialMediaAgg.InstagramAgg;
using Query.SocialMedia.Instagram.DTOs;

namespace Query.SocialMedia.Instagram
{
    public static class InstagramMapper
    {
        public static InstagramDto? MapInstagram(this Domain.SocialMediaAgg.InstagramAgg.Instagram? model)
        {
            return new InstagramDto()
            {
                Id = model.Id,
                CreationDate = model.CreationDate,
                accessToken = model.accessToken,
                InstagramName = model.accessToken,
                UserName = model.UserName,
                SendMethod = model.SendMethod
            };
        }
    }
}
