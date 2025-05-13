using Common.Domain;
using Common.Domain.Exceptions;

namespace Domain.SocialMediaAgg.InstagramAgg.Post
{
    public class Post : BaseEntity
    {
        public DateTime DateOfPosting { get; private set; }
        //public string Picture { get; private set; }
        public string Description { get; private set; }
        //public string ImageName { get; private set; }
        public string? InstagramPostId { get; set; }
        //public string VideoName { get; private set; }
        public string InstagramUserName { get; set; }
        //public string Title { get; private set; }
        public string Link { get; private set; }
        // public string Slug { get; private set; }
        public bool IsSend { get; private set; } = false;
        //public long InstagramId { get; internal set; } //InstagramPostId OR TelegramPostId
                                                          // [NotMapped]
        public List<InstagramPostImage> Images { get; private set; } = new List<InstagramPostImage>();
        //[NotMapped]                                               
        public List<InstagramPostVideo> Videos { get; private set; } = new List<InstagramPostVideo>();

        private Post()
        {

        }
        public Post(DateTime dateOfPosting, string description, string link)
        {
            DateOfPosting = dateOfPosting;
            Description = description;
            Link = link;
            // Slug = slug?.ToSlug(); string slug,
            //ImageName = imageName;
            //VideoName = videoName;
        }
        public void Edit(DateTime dateOfPosting, string description, string link)
        {
            DateOfPosting = dateOfPosting;
            Description = description;
            Link = link;
            //Slug = slug?.ToSlug(); string slug,
            //ImageName = imageName;
            //VideoName = videoName;
        }
        //public void SetPostImage(string imageName)
        //{
        //    NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        //    ImageName = imageName;
        //}

        public void AddImage(List<string> imageName)
        {
            for (int i = 1; i <= imageName.Count(); i++)
            {
                var image = new InstagramPostImage(imageName[i - 1], Images.Count + 1);

                image.PostId = Id;
                Images.Add(image);
            }
        }
        //public void AddImagesequence(List<string> imageName)
        //{
        //    for (int i = 1; i <= imageName.Count(); i++)
        //    {
        //        var image = new InstagramPostImage(imageName[i-1], Images.Count()+i);

        //        image.PostId = Id;
        //        Images.Add(image);
        //    }
        //}
        //public void SetPostVideo(string imageName)
        //{
        //    NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        //    ImageName = imageName;
        //}

        public void AddVideo(List<string> videoName)
        {
            for (int i = 1; i <= videoName.Count(); i++)
            {
                var video = new InstagramPostVideo(videoName[i - 1], Images.Count + 1);
                video.PostId = Id;
                Videos.Add(video);
            }
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

