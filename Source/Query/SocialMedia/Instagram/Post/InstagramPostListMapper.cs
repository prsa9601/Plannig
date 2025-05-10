using Domain.SocialMediaAgg.InstagramAgg.Post;
using Query.SocialMedia.Instagram.DTOs;
using Query.SocialMedia.Instagram.Post.DTOs;
using static Query.SocialMedia.Instagram.Post.DTOs.PostFilterData;

namespace Query.SocialMedia.Instagram.Post
{
    public static class InstagramPostListMapper 
    {
        public static PostDto PostMap(this Domain.SocialMediaAgg.InstagramAgg.Post.Post? post)
        {
            
            return new PostDto()
            {
                CreationDate = post.CreationDate,
                DateOfPosting = post.DateOfPosting,
                Description = post.Description,
                Id = post.Id,
                //ImageName = post.ImageName,
                Images = post.Images.ImageMap(),
                InstagramPostId = post.InstagramPostId,
                Videos = post.Videos.VideoMap(),
                Link = post.Link,
                InstagramUserName = post.InstagramUserName,
                IsSend = post.IsSend,
                
            };
        }
        public static InstagramPostFilterData PostFilterMap(this Domain.SocialMediaAgg.InstagramAgg.Post.Post? post)
        {
            
            return new InstagramPostFilterData()
            {
                CreationDate = post.CreationDate,
                DateOfPosting = post.DateOfPosting,
                Description = post.Description,
                Id = post.Id,
                //ImageName = post.ImageName,
                Images = post.Images.ImageMap(),
                InstagramPostId = post.InstagramPostId,
                Videos = post.Videos.VideoMap(),
                Link = post.Link,
                InstagramUserName = post.InstagramUserName,
                IsSend = post.IsSend,
                
            };
        }
        public static PostDto FilterPostMap(this Domain.SocialMediaAgg.InstagramAgg.Post.Post? post)
        {
            
            return new PostDto()
            {
                CreationDate = post.CreationDate,
                DateOfPosting = post.DateOfPosting,
                Description = post.Description,
                Id = post.Id,
                //ImageName = post.ImageName,
                Images = post.Images.ImageMap(),
                InstagramPostId = post.InstagramPostId,
                Videos = post.Videos.VideoMap(),
                Link = post.Link,
                IsSend = post.IsSend,
                InstagramUserName = post.InstagramUserName,
            };
        }
        internal static List<PostVideoDto?> VideoMap(this List<InstagramPostVideo?> posts)
        {
            List<PostVideoDto?> model = new List<PostVideoDto?>();

            foreach (var item in posts)
            {
                var dto = new PostVideoDto()
                {
                    CreationDate = item.CreationDate,
                    Id = item.Id,
                    PostId = item.PostId,
                    Secuence = item.Sequence,
                    VideoName = item.VideoName,
                    Link = item.Link
                };
                model.Add(dto);
            }

            return model;
        }
        internal static List<PostImageDto?> ImageMap(this List<InstagramPostImage?> posts)
        {
            List<PostImageDto?> model = new List<PostImageDto?>();

            foreach (var item in posts)
            {
                var dto = new PostImageDto()
                {
                    CreationDate = item.CreationDate,
                    Id = item.Id,
                    ImageName = item.ImageName,
                    PostId = item.PostId,
                    Secuence = item.Seqence,
                    Link = item.Link
                };
                model.Add(dto);
            }

            return model;
        }
    }
}
