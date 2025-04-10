using Domain.BlogAgg;
using Query.Blog.DTOs;

namespace Query.Blog
{
    public static class BlogMapper
    {
        public static BlogFilterDataDto? MapFilter(this Domain.BlogAgg.Blog? blog)
        {
            return new BlogFilterDataDto
            {
                Id = blog!.Id,
                CreationDate = blog.CreationDate,
                CreatorUserName = blog.CreatorUserName,
                Description = blog.Description,
                ImageName = blog.ImageName,
                IsSend = blog.IsSend,
                SendTime = blog.SendTime,
                SeoData = blog.SeoData,
                Slug = blog.Slug,
                Title = blog.Title,
                Visit = blog.Visit + 1,
                CategoryId = blog.CategoryId,
            };
        }
        public static BlogDto? Map(this Domain.BlogAgg.Blog? blog)
        {
            return new BlogDto
            {
                Id = blog!.Id,
                CreationDate = blog.CreationDate,
                CreatorUserName = blog.CreatorUserName,
                Description = blog.Description,
                ImageName = blog.ImageName,
                IsSend = blog.IsSend,
                SendTime = blog.SendTime,
                SeoData = blog.SeoData,
                Slug = blog.Slug,
                Title = blog.Title,
                Visit = blog.Visit + 1,
                CategoryId = blog.CategoryId,
            };
        }
    }
}
