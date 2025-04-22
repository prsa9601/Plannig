namespace Domain.SocialMediaAgg.InstagramAgg.Service
{
    public interface IInstagramService
    {
        Task<bool> InstagramAccountExist(string userName, string accessToken);
        Task<bool> InstagramPageExist(string pageId);
        Task<bool> InstagramExistInDataBase(string instagramId, string pageId);
    }
}
