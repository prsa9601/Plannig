using Common.Domain.Repository;

namespace Domain.SocialMediaAgg.TelegramAgg.Repository
{
    public interface ITelegramRepository : IBaseRepository<Telegram>
    {
        //Task<int> Delete(long id);
        Task<int> DeleteTelegram(int postId);
        Task<int> SendMessageToTelegram(string channelName, string caption, string token);
        Task<int> SendImageToTelegram(string channelName, string caption
            , string imagePath, long id, string token);
        Task<int> SendVideoToTelegram(string channelName, string videoCaption
            , string videoPath, long id, int width, int height
                  , string thumbnail, string token);

    }
}
