using Common.Domain;

namespace Domain.SocialMediaAgg.InstagramAgg.Story;

public class StoryVideo : BaseEntity
{
    // public DateTime DateOfPosting { get; private set; }
    public string VideoPath { get; private set; }
    //public int Secuence { get; private set; }
    public string? Link { get; private set; }
   // public long StoryId { get; internal set; }


    public StoryVideo(string videoPath)
    {
        // DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        VideoPath = videoPath;
        // Secuence = secuence;, int secuence
        //Link = link;
       // StoryId = storyId;, long storyId
    }
    public void Edit(string videoPath)
    {
        // DateOfPosting = dateOfPosting; DateTime dateOfPosting,
        VideoPath = videoPath;
        //Secuence = secuence;, int secuence
        //Link = link;
    }
    //public StoryVideo(string videoPath, int secuence, string link)
    //{
    //    // DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
    //    VideoPath = videoPath;
    //    Secuence = secuence;
    //    Link = link;
    //}
    //public void Edit(string videoPath, int secuence, string link)
    //{
    //    // DateOfPosting = dateOfPosting; DateTime dateOfPosting,
    //    VideoPath = videoPath;
    //    Secuence = secuence;
    //    Link = link;
    //}
}