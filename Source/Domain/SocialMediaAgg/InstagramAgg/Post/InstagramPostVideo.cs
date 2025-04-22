using Common.Domain;

namespace Domain.SocialMediaAgg.InstagramAgg.Post;

public class InstagramPostVideo : BaseEntity
{
    private InstagramPostVideo()
    {
        
    }
    //public DateTime DateOfPosting { get; private set; }
    public string VideoName { get; private set; }
    public long PostId { get; internal set; }
    public int Sequence { get; private set; }
    public string? Link { get; private set; }

    public InstagramPostVideo(string videoName, int sequence)
    {
        //DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        VideoName = videoName;
        Sequence = sequence;
        //Link = link;
    }
    public void Edit(string videoName, int sequence, string link)
    {
        // DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        VideoName = videoName;
        Sequence = sequence;
        Link = link;
    }
}