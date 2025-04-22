using Common.Domain;

namespace Domain.SocialMediaAgg.InstagramAgg.Post;

public class InstagramPostImage : BaseEntity
{
    private InstagramPostImage()
    {
        
    }
    //public DateTime DateOfPosting { get; private set; }
    public string ImageName { get; private set; }
    public long PostId { get; internal set; }
    public int Seqence { get; private set; }
    public string? Link { get; private set; } //LinkPost

    public InstagramPostImage(string imageName, int sequence)
    {
        //DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        ImageName = imageName;
        Seqence = sequence;
        //Link = "Default";
    }
    public void Edit(string imageName, int sequence, string link)
    {
        // DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        ImageName = imageName;
        Seqence = sequence;
        Link = link;
    }
}