using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SocialMediaAgg.InstagramAgg.Post;
using Domain.SocialMediaAgg.TelegramAgg.Post;
using Query.SocialMedia.Telegram.Post.DTOs;

namespace Query.SocialMedia.Telegram.Post
{
    internal static class TelegramPostMapper
    {
        internal static PostDto? PostMap(this Domain.SocialMediaAgg.TelegramAgg.Post.Post post)
        {
            return new PostDto()
            {
                CreationDate = post.CreationDate,
                DateOfPosting = post.DateOfPosting,
                Description = post.Description,
                Id = post.Id,
                ImageName = post.ImageName,
                Images = post.Images.ImageMap(),
                postId = post.postId,
                Videos = post.Videos.VideoMap(),
                Link = post.Link
            };
        }
        internal static List<PostVideoDto?> VideoMap(this List<TelegramPostVideo?> posts)
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
        internal static List<PostImageDto?> ImageMap(this List<TelegramPostImage?> posts)
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
