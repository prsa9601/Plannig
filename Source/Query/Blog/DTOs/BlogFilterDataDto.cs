using Common.Domain.ValueObjects;
using Common.Query;
using Common.Query.Filter;

namespace Query.Blog.DTOs
{
    public class BlogFilterDataDto : BaseDto
    {
        public string Slug { get; set; }
        public string ImageName { get; set; }
        public DateTime SendTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorUserName { get; set; }
        public SeoData SeoData { get; set; }
        public bool IsSend { get; set; }
        public int Visit { get; set; }
        public long CategoryId { get; set; }
    }
    public class BlogFilterParam : BaseFilterParam
    {
        public string? Slug { get; set; } = "";
        public string? Search { get; set; } = "";
        public PostSearchOrderBy? SearchOrderBy { get; set; }
        public string? Title { get; set; }
        public long CategoryId { get; set; }
    }
    public class BlogFilterResult : BaseFilter<BlogFilterDataDto, BlogFilterParam>
    {
    }

    public enum PostSearchOrderBy
    {
        visit,
        latest
    }
}
