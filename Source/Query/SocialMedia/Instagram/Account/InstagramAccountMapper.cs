using Domain.SocialMediaAgg.InstagramAgg;
using Domain.SocialMediaAgg.InstagramAgg.Post;
using Domain.SocialMediaAgg.InstagramAgg.Story;
using Query.SocialMedia.Instagram.Account.DTOs;

namespace Query.SocialMedia.Instagram.Account
{
    internal static class InstagramAccountMapper
    {
        internal static InstagramAccountDto? Map(
            this Domain.SocialMediaAgg.InstagramAgg.Instagram instagram)
        {
            return new InstagramAccountDto
            {
                Id = instagram.Id,
                InstagramId = instagram.InstagramId,
                accessToken = instagram.accessToken,
                CreationDate = instagram.CreationDate,
                Profile = instagram.Profile,
                PageId = instagram.PageId,
                InstagramUserName = instagram.InstagramUserName,
                Posts = instagram.Posts!.MapPosts()!,
                //SendMethod = instagram.SendMethod,
                Stories = instagram.Stories.MapStories(),
                UserName = instagram.UserName,
            };
        }
        internal static List<InstagramAccountPostDto?> MapPosts(
            this List<Domain.SocialMediaAgg.InstagramAgg.Post.Post?> post)
        {
            var Posts = new List<InstagramAccountPostDto?>();
            foreach (var postItem in post) 
            {
                var model = new InstagramAccountPostDto
                {
                    //InstagramId = postItem!.InstagramId,
                    PostId = postItem.InstagramPostId!,
                    Id = postItem.Id,
                    DateOfPosting = postItem.DateOfPosting,
                    Description = postItem.Description,
                    CreationDate = postItem.CreationDate,
                    //ImageName = postItem.ImageName,
                    //Images = postItem.Images!.MapPostImage()!,
                    InstagramUserName = postItem.InstagramUserName,
                    IsSend = postItem.IsSend,
                    Link = postItem.Link,
                    //VideoName = postItem.VideoName,
                    Videos = postItem.Videos!.MapPostVideo()!
                };
                Posts.Add(model);
            }
            return Posts;
        }
        internal static List<InstagramAccountStoryDto?> MapStories(
            this List<Domain.SocialMediaAgg.InstagramAgg.Story.Story?> story)
        {
            var Posts = new List<InstagramAccountStoryDto?>();
            foreach (var storyItem in story) 
            {
                var model = new InstagramAccountStoryDto
                {
                    InstagramId = storyItem!.InstagramId,
                    storyId = storyItem.storyId,
                    Id = storyItem.Id,
                    DateOfPosting = storyItem.DateOfPosting,
                    CreationDate = storyItem.CreationDate,
                    //ImageName = storyItem.ImageName,
                    Images = storyItem.Image?.MapStoryImage()!,
                    //InstagramUserName = storyItem.InstagramUserName,
                    IsSend = storyItem.IsSend,
                    Link = storyItem.Link,
                    Videos = storyItem.Video?.MapStoryVideo()!
                };
                Posts.Add(model);
            }
            return Posts;
        }
        internal static List<InstagramPostVideoDto?> MapPostVideo(this 
            List<InstagramPostVideo?> video)
        {
            var Videos = new List<InstagramPostVideoDto?>();
            foreach (var item in video)
            {
                var model = new InstagramPostVideoDto
                {
                    CreationDate = item!.CreationDate,
                    Id = item.Id,
                    Link = item.Link,
                    PostId = item.PostId,
                    Sequence = item.Sequence,
                    VideoName = item.VideoName,
                };
                Videos.Add(model);
            }
            return Videos;
        }
        internal static List<InstagramPostImageDto?> MapPostImage(this 
            List<InstagramPostImage?> images)
        {
            var Images = new List<InstagramPostImageDto?>();
            foreach (var item in images)
            {
                var model = new InstagramPostImageDto
                {
                    CreationDate = item!.CreationDate,
                    Id = item.Id,
                    Link = item.Link,
                    PostId = item.PostId,
                    Sequence = item.Seqence,
                    ImageName = item.ImageName,
                };
                Images.Add(model);
            }
            return Images;
        }
        internal static InstagramStoryImageDto? MapStoryImage(this StoryImage? image)
        {
            return new InstagramStoryImageDto
            {
                Id = image!.Id,
                CreationDate = image.CreationDate,
                Link = image.Link,
                PictureName = image.PictureName,
            };
        }
        internal static InstagramStoryVideoDto? MapStoryVideo(this StoryVideo? image)
        {
            return new InstagramStoryVideoDto
            {
                Id = image!.Id,
                CreationDate = image.CreationDate,
                Link = image.Link,
                VideoPath = image.VideoPath,
            };
        }
    }
}
