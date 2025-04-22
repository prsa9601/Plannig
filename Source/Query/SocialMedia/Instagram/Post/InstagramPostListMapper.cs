using Domain.SocialMediaAgg.InstagramAgg.Post;
using Query.SocialMedia.Instagram.DTOs;
using Query.SocialMedia.Instagram.Post.DTOs;

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
                postId = post.PostId,
                Videos = post.Videos.VideoMap(),
                Link = post.Link
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
                postId = post.PostId,
                Videos = post.Videos.VideoMap(),
                Link = post.Link
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
                    Link = item.Link
                };
                model.Add(dto);
            }

            return model;
        }
    }
}
