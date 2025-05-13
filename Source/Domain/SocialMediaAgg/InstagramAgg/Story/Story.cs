using Common.Domain;
using Common.Domain.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.SocialMediaAgg.InstagramAgg.Story;

public class Story : BaseEntity
{
    public string? storyId { get; private set; } //InstagramPostId OR TelegramPostId
    public DateTime DateOfPosting { get; private set; }
    //public string Picture { get; private set; }
    //public string Discription { get; private set; }
    public string Link { get; private set; }
    public bool IsSend { get; private set; }
    //public string ImageName { get; private set; }
    public long InstagramId { get; set; }
    public string? InstagramUserName { get; set; }
    //public string? Slug { get; private set; }  
    public StoryImage? Image { get; private set; }
    public StoryVideo? Video { get; private set; }

    public Story(DateTime dateOfPosting, string link)
    {
        //ImageName = imageName;, string imageName
        DateOfPosting = dateOfPosting;
        Link = link;
    }
    //public void SetProductImage(string imageName)
    //{
    //    NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    //    ImageName = imageName;
    //}
    public void SetImage(string imageName)
    {
        var image = new StoryImage(imageName);
        Image = image;
    }
    public void RemoveImage()
    {
        Image = null;
    }
    public void RemoveVideo()
    {
        Video = null;
    }
    public void SetVideo(string videoName)
    {
        var video = new StoryVideo(videoName);
        Video = video;
    }
    //public void AddImage(PostImage image)
    //{
    //    image.PostId = Id;
    //    Images.Add(image);
    //}

    //public string RemoveImage(long id)
    //{
    //    var image = Images.FirstOrDefault(f => f.Id == id);
    //    if (image == null)
    //        throw new NullOrEmptyDomainDataException("عکس یافت نشد");

    //    Images.Remove(image);
    //    return image.ImageName;
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
    public void Edit(DateTime dateOfPosting, string link)
    {
        //ImageName = imageName;, string imageName
        DateOfPosting = dateOfPosting;
        Link = link;
    }
    //public void ChangeImage(string imagePath)
    //{
    //    ImageName = imagePath;
    //}
    //public void DeleteImage()
    //{
    //    ImageName = "";
    //}
}