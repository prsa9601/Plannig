using Common.Domain;

namespace Domain.SocialMediaAgg.InstagramAgg.Story;

public class StoryImage : BaseEntity
{
    // public DateTime DateOfPosting { get; private set; }
    public string PictureName { get; private set; }
    //public int Secuence { get; private set; }
    public string Link { get; private set; }
   // public long StoryId { get; internal set; }

    public StoryImage(string pictureName, string link)
    {
        // DateOfPosting = dateOfPosting;DateTime dateOfPosting, 
        PictureName = pictureName;
        Link = link;
        //StoryId = storyId;, long storyId
    }
    public void Edit(string pictureName, int secuence, string link)
    {
        // DateOfPosting = dateOfPosting; DateTime dateOfPosting,
        PictureName = pictureName;
        Link = link;
    }
}