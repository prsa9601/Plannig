using Common.Domain.Repository;

namespace Domain.SocialMediaAgg.InstagramAgg.Repository
{
    public interface IInstagramRepository : IBaseRepository<Instagram>
    {
        Task<Instagram?> GetTrackingByUserName(string userName);
        Task<int> Delete(long id);
        Task<Instagram?> GetTrackingByInstagramId(string id);
        Task<int> PostToInstagram(string accessToken, string imageUrl, string caption);
        Task<int> DeleteStory(string InstagramId, int postId, string accessToken, string imagePath, string token);
        Task<int> UploadStory(string InstagramId, string accessToken, string imagePath, string token);
    }
}
