using Common.Domain;

namespace Domain.SocialMediaAgg.InstagramAgg.Post;

public class InstagramPostImage : BaseEntity
{
    private InstagramPostImage()
    {
        
    }
    //public DateTime DateOfPosting { get; private set; }
    public string ImageName { get; private set; }
    public string PostId { get; internal set; }
    public int Secuence { get; private set; }
    public string Link { get; private set; } //LinkPost

    public InstagramPostImage(string imageName, int secuence)
    {
        //DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        ImageName = imageName;
        Secuence = secuence;
        // Link = link;, string link
    }
    public void Edit(string imageName, int secuence, string link)
    {
        // DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        ImageName = imageName;
        Secuence = secuence;
        Link = link;
    }
}