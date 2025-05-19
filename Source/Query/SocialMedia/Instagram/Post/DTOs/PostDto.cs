using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.SocialMedia.Instagram.Post.DTOs
{

    public class PostDto : BaseDto
    {
        public DateTime DateOfPosting { get; set; }
        //public string Picture { get; private set; }
        public string Description { get; set; }
        //public string ImageName { get; set; }
        public string InstagramUserName { get; set; } // UserName or PageName
        public string Link { get; set; }
        //public string Slug { get; set; }
        public bool IsSend { get; set; }
        public string? InstagramPostId { get; set; } //InstagramPostId OR TelegramPostId
        public List<PostVideoDto> Videos { get; set; }
        //public List<PostImageDto> Images { get; set; }
    }
    public class PostImageDto : BaseDto
    {
        //public DateTime DateOfPosting { get;   set; }
        public string ImageName { get; set; }
        public long PostId { get; set; }
        public int Secuence { get; set; }
        public string? Link { get; set; }
    }

    public class StoryImageDto : BaseDto
    {
        // public DateTime DateOfPosting { get; private set; }
        public string Picture { get; set; }
        public int Secuence { get; set; }
        public string? Link { get; set; }
    }



    //public enum SendMedia
    //{
    //    Instagram,
    //    Telegram
    //}

    public class PostVideoDto : BaseDto
    {
        //public DateTime DateOfPosting { get; private set; }
        public string VideoName { get; set; }
        public long PostId { get; set; }
        public int Secuence { get; set; }
        public string? Link { get; set; }

    }
    public class StoryVideoDto : BaseDto
    {
        // public DateTime DateOfPosting { get; private set; }
        public string VideoPath { get; set; }
        //public int Secuence { get; set; }
        public string Link { get; set; }

    }
    //public class TelegramDto : BaseDto
    //{
    //    //Telegram
    //    public string token { get; set; } //token Telegram
    //    public string chat_id { get; set; } //TelegramID
    //    public string UserName { get; set; } //token Telegram
    //    public List<PostDto> Posts { get; set; }
    //    public SendMethodTelegram SendMethod { get; set; }

    //}

}
