using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.DTOs;
using System.Collections.Generic;
using Query.SocialMedia.Instagram.Post.DTOs;

namespace Query.SocialMedia.Instagram.Post.GetByFilter
{
    public class GetInstagramPostByFilterQueryHandler : IQueryHandler<GetInstagramPostByFilterQuery, PostFilterData.InstagramPostFilterResult>
    {
        private readonly PlanningContext _context;

        public GetInstagramPostByFilterQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<PostFilterData.InstagramPostFilterResult> Handle(GetInstagramPostByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Instagram.Include
                (i => i.Posts).SelectMany(i => i.Posts!).AsQueryable(); 
            //Include(i => i.Posts).SelectMany(i => i.Posts);
            //var posts = _context.Instagram.OrderByDescending(d => d.Id).Include("Posts").ToList();

            //List<Domain.SocialMediaAgg.InstagramAgg.Post.Post> postList = new List<Domain.SocialMediaAgg.InstagramAgg.Post.Post>();
            // List<Domain.SocialMediaAgg.InstagramAgg.Post.Post> postListResult = new List<Domain.SocialMediaAgg.InstagramAgg.Post.Post>();
            // List<Domain.SocialMediaAgg.PostAgg.Post> result = new List<Domain.SocialMediaAgg.PostAgg.Post>();
            //foreach (var item in posts)
            //{
            //    postList.AddRange(item.Posts);
            //}
            //var p = posts.Select(i => i.Posts.FirstOrDefault(i => i.ImageName));
            //result.Select(i => i.Select(p => p.Description));
            var postList = //(IQueryable<Domain.SocialMediaAgg.InstagramAgg.Post.Post>)
                result.Select(i => i).AsQueryable();
            //postList.AddRange(
            //    (IQueryable<Domain.SocialMediaAgg.InstagramAgg.Post.Post>)
            //    result.Select(i => i).ToList());

            //if (!string.IsNullOrWhiteSpace(@params.Search))
            //    postListResult.AddRange(postList.Where(p =>
            //        p.Description.Contains(@params.Search)));

            if (!string.IsNullOrWhiteSpace(@params.Search))
            {
                postList = postList.Where(p =>
                    p.Description.Contains(@params.Search));
            }

            if (@params.InstagramId > 0 && @params.InstagramId != null)
            {
                //var instagramPost = await _context.Instagram.
                //    FirstOrDefaultAsync(i => i.Id == @params.InstagramId, cancellationToken);
                //postList = (IQueryable<Domain.SocialMediaAgg.InstagramAgg.Post.Post>)instagramPost!.Posts!.ToList();


                var instagramPost = await _context.Instagram
                    .FirstOrDefaultAsync(i => i.Id == @params.InstagramId, cancellationToken);

                if (instagramPost?.Posts != null)
                    postList = instagramPost.Posts.AsQueryable();
                //postList = postList.Where(p =>
                //    p.insta == (@params.InstagramId));
            }

            //if (!string.IsNullOrWhiteSpace(@params.Title))
            //    postListResult.AddRange(postList.Where(p => p.Description.Contains(@params.Search)));


            switch (@params.InstagramPostSearchOrderBy)
            {
                case Query.SocialMedia.Instagram.Post.DTOs.PostFilterData.InstagramPostSearchOrderBy.latest:
                    {
                        postList = postList.OrderByDescending(r => r.CreationDate);

                        //postListResult = postListResult.OrderByDescending(r => r.CreationDate).ToList();

                        //postListResult = (List<Domain.SocialMediaAgg.InstagramAgg.Post.Post>)postListResult.OrderByDescending(r => r.CreationDate);
                        break;
                    }
                default:
                {
                    postList = postList.OrderByDescending(i=>i.Id);
                    break;
                }
                    //case PostSearchOrderBy.visit:
                    //    {
                    //        result = result.OrderByDescending(r => r.Visit);
                    //        break; 
                    //    }
            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new PostFilterData.InstagramPostFilterResult()
            {
                Data = postList.Skip(skip).Take(@params.Take).Select(s => s.PostFilterMap())
                    .ToList(),    
                //.ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(postList, @params.Take, @params.PageId);
            return model;
        }
    }
} 
