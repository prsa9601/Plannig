using Domain.SocialMediaAgg.TelegramAgg;
using Domain.SocialMediaAgg.TelegramAgg.Post;
using Query.SocialMedia.Telegram.Account.DTOs;

namespace Query.SocialMedia.Telegram.Account
{
    public static class TelegramAccountMapper
    {
        public static TelegramAccountDto? Map
            (this Domain.SocialMediaAgg.TelegramAgg.Telegram entity)
        {
            return new TelegramAccountDto
            {
                Id = entity.Id,
                Chat_Id = entity.Chat_Id,
                CreationDate = entity.CreationDate,
                Posts = entity.Posts!.PostMap()!,
                SendMethod = entity.SendMethod,
                TelegramChannelMethod = entity.TelegramChannelMethod,
                TelegramProfiles = entity.TelegramProfiles!.ProfileMap()!,
                Token = entity.Token,
                UserName = entity.UserName,
            };
        }
        private static List<TelegramProfileDto?> ProfileMap(this List<TelegramProfile?> model)
        {
            List<TelegramProfileDto> telegramProfiles = new List<TelegramProfileDto>();
            foreach (var profile in model)
            {
                var profileModel = new TelegramProfileDto
                {
                    Id = profile!.Id,
                    CreationDate = profile.CreationDate,
                    ImageName = profile.ImageName,
                    TelegramId = profile.TelegramId,
                };
                telegramProfiles.Add(profileModel);
            }
            return telegramProfiles!;
        }
        //List<>?
        private static List<PostDto?> PostMap(this List<Domain.SocialMediaAgg.TelegramAgg.Post.Post?> model)
        {
            List<PostDto> Posts = new List<PostDto>();
            foreach (var post in model)
            {
                var postModel = new PostDto
                {
                    Id = post!.Id,
                    CreationDate = post.CreationDate,
                    DateOfPosting = post.DateOfPosting,
                    Description = post.Description,
                    ImageName = post.ImageName,
                    Images = post.Images!.TelegramImagePostMap()!,
                    IsSend = post.IsSend,
                    Link = post.Link,
                    postId = post.postId,
                    TelegramUserName = post.TelegramUserName,
                    VideoName = post.VideoName,
                    Videos = post.Videos!.TelegramVideoPostMap()!,
                };
                Posts.Add(postModel);
            }
            return Posts!;   
        }
        private static List<TelegramPostImageDto?> TelegramImagePostMap(this List<TelegramPostImage?> model)
        {
            List<TelegramPostImageDto> Images = new List<TelegramPostImageDto>();
            foreach (var image in model)
            {
                var imageModel = new TelegramPostImageDto
                {
                    Id = image!.Id,
                    CreationDate = image.CreationDate,
                    ImageName = image.ImageName,
                    Link = image.Link,
                    PostId = image.PostId,
                    Secuence = image.Secuence,
                };
                Images.Add(imageModel);
            }
            return Images!;

        }
        private static List<TelegramPostVideoDto?> TelegramVideoPostMap(this List<TelegramPostVideo?> model)
        {
            List<TelegramPostVideoDto> Videos = new List<TelegramPostVideoDto>();
            foreach (var video in model)
            {
                var videoModel = new TelegramPostVideoDto
                {
                    Id = video!.Id,
                    CreationDate = video.CreationDate,
                    Link = video.Link,
                    PostId = video.PostId,
                    Sequence = video.Sequence,
                    VideoName = video.VideoName,
                };
                Videos.Add(videoModel);
            }
            return Videos!;
        }
    }
}
