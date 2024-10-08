using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.SocialMediaAgg.InstagramAgg.Post;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.SocialMediaAgg.TelegramAgg.Post
{
    public class Post : BaseEntity
    {
        public DateTime DateOfPosting { get; private set; }
        //public string Picture { get; private set; }
        public string Description { get; private set; }
        public string ImageName { get; private set; }
        public string VideoName { get; private set; }
        public string TelegramUserName { get; set; } //channelAddress Or Group

        //public string Title { get; private set; }
        public string Link { get; private set; }
        // public string Slug { get; private set; }
        public bool IsSend { get; private set; } = false;
        public string postId { get; private set; } //InstagramPostId OR TelegramPostId
        //[NotMapped]
        public List<TelegramPostImage> Images { get; private set; }
        //[NotMapped]
        public List<TelegramPostVideo> Videos { get; private set; }

        private Post()
        {
            Images = new List<TelegramPostImage>();
            Videos = new List<TelegramPostVideo>();
        }
        public Post(DateTime dateOfPosting, string description, string link, string imageName, string videoName)
        {
            DateOfPosting = dateOfPosting;
            Description = description;
            Link = link;
            // Slug = slug?.ToSlug(); string slug,
            ImageName = imageName;
            VideoName = videoName;
        }
        public void Edit(DateTime dateOfPosting, string description, string link, string imageName, string videoName)
        {
            DateOfPosting = dateOfPosting;
            Description = description;
            Link = link;
            //Slug = slug?.ToSlug(); string slug,
            ImageName = imageName;
            VideoName = videoName;
        }
        public void SetPostImage(string imageName)
        {
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            ImageName = imageName;
        }

        public void AddImage(TelegramPostImage image)
        {
            image.PostId = Id;
            Images.Add(image);
        }

        public void SetPostVideo(string imageName)
        {
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            ImageName = imageName;
        }

        public void AddVideo(TelegramPostImage image)
        {
            image.PostId = Id;
            Images.Add(image);
        }

        public string RemoveImage(long id)
        {
            var image = Images.FirstOrDefault(f => f.Id == id);
            if (image == null)
                throw new NullOrEmptyDomainDataException("عکس یافت نشد");

            Images.Remove(image);
            return image.ImageName;
        }
        //public void Edit(DateTime dateOfPosting, string discription, string link)
        //{
        //    DateOfPosting = dateOfPosting;
        //    Discription = discription;
        //    Link = link;
        //}
        public void Send()
        {
            if (DateTime.Now >= DateOfPosting)
            {
                IsSend = true;
            }
            else
                throw new Exception("ارسال پست در  این زمان مجاز نیست!");
        }
    }
}

