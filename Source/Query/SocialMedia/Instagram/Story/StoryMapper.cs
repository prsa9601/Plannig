using Domain.SocialMediaAgg.InstagramAgg.Post;
using Query.SocialMedia.Instagram.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SocialMediaAgg.InstagramAgg.Story;
using Query.SocialMedia.Instagram.Story.DTOs;

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
                ImageName = story.ImageName,
                Images = story.Images.ImageMap(),
                Videos = story.Videos.VideoMap(),
                Link = story.Link
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
