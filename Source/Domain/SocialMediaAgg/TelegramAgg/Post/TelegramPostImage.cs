using Common.Domain;

namespace Domain.SocialMediaAgg.TelegramAgg.Post;

public class TelegramPostImage : BaseEntity
{
    //public DateTime DateOfPosting { get; private set; }
    public string ImageName { get; private set; }
    public long PostId { get; internal set; }
    public int Secuence { get; private set; }
    //public string? Link { get; private set; }

    public TelegramPostImage(string imageName, int secuence)
    {
        //DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        ImageName = imageName;
        Secuence = secuence;
        //Link = link;, string? link
    }

    public void Edit(string imageName, int secuence)
    {
        // DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        ImageName = imageName;
        Secuence = secuence;
        //Link = link;, string? link
    }
}