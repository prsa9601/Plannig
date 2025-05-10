using Domain.SocialMediaAgg.InstagramAgg.Post;
using Query.SocialMedia.Instagram.Post;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Utils;
using Domain.SocialMediaAgg.InstagramAgg.Story;
using Query.SocialMedia.Instagram.Story.DTOs;
using Domain.SocialMediaAgg.InstagramAgg;

namespace Query.SocialMedia.Instagram.Story
{
    public static class StoryMapper
    {
        public static StoryDto StoryMap(this Domain.SocialMediaAgg.InstagramAgg.Story.Story? story)
        {   
            return new StoryDto()
            {
                CreationDate = story.CreationDate,
                DateOfPosting = story.DateOfPosting,
                Id = story.Id,
                //InstagramId = story.InstagramId,
                //InstagramUserName = story.InstagramUserName
                //ImageName = story.ImageName,
                Images = story.Image.ImageMap()!,
                Videos = story.Video.VideoMap()!,
                Link = story.Link,
                IsSend = story.IsSend,
                InstagramId = story.InstagramId,
                InstagramStoryId = story.storyId,
                InstagramUserName = story.InstagramUserName,
                SendMethod = SendMethodInstagramForPost.Story,
                
            };
        }
        internal static StoryVideoDto? VideoMap(this StoryVideo? story)
        {    
            return new StoryVideoDto()
            { 
                CreationDate = story.CreationDate, 
                Id = story.Id, 
                Link = story.Link, 
                VideoPath = story.VideoPath
            };
            

           
        }
        internal static StoryImageDto? ImageMap(this StoryImage? story)
        {
          
            return new StoryImageDto()
            {
                CreationDate = story.CreationDate,
                Id = story.Id,
                Link = story.Link,
                PictureName = story.PictureName
            };
            

        }
    }
}
