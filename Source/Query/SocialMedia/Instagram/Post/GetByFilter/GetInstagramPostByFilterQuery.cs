using Common.Query;
using Query.SocialMedia.Instagram.Post.DTOs;

namespace Query.SocialMedia.Instagram.Post.GetByFilter
{
    public class GetInstagramPostByFilterQuery : QueryFilter<Query.SocialMedia.Instagram.Post.DTOs.PostFilterData.InstagramPostFilterResult, Query.SocialMedia.Instagram.Post.DTOs.PostFilterData.InstagramPostFilterParam>
    {
        public GetInstagramPostByFilterQuery(Query.SocialMedia.Instagram.Post.DTOs.PostFilterData.InstagramPostFilterParam filterParams) : base(filterParams)
        {
        }
    }
}
