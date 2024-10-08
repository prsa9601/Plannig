using Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;
using NReco.VideoInfo;
using Telegram.Bot.Types;
using Telegram.Bot;
using Domain.SocialMediaAgg.TelegramAgg.Repository;

namespace Infrastructure.Persistent.Ef.TelegramAgg
{
    public class TelegramRepository :BaseRepository<Domain.SocialMediaAgg.TelegramAgg.Telegram>, ITelegramRepository
    {
        public TelegramRepository(PlanningContext context) : base(context)
        {
        }
        public async Task<int> SendVideoToTelegram(string channelName, string videoCaption, string videoPath, long id, int width, int height, string thumbnail, string token)
        {
            try
            {
                var bot = new TelegramBotClient("کلید API شما");
                id += 7;
                //var photoStream = new FileStream(imagePath, FileMode.Open);
                //var t = await bot.SendPhotoAsync("@channelname یا chat_id", new InputOnlineFile(photoStream), caption);
                // ارسال فیلم به کانال (با نام کاربری یا chat_id)
                // var videoStream = new FileStream("path/to/your/video.mp4", FileMode.Open);var videoPath = "path/to/your/video.mp4";

                var ffProbe = new FFProbe();
                var videoInfo = ffProbe.GetMediaInfo(videoPath);
                var duration = videoInfo.Duration.Seconds;


                // ارسال ویدیو به کانال (با نام کاربری یا chat_id)
                var videoStream = new FileStream(videoPath, FileMode.Open);
                var video = new InputFileStream(videoStream, thumbnail);
                var t = await bot.SendVideoAsync("@channelname یا chat_id", video, (Int32)id
                    , duration, width, height, video, videoCaption);
                return 200;
            }
            catch (Exception e)
            {
                return 500;
            }
        }
        public async Task<int> SendImageToTelegram(string channelName, string caption, string imagePath, long id, string token)
        {
            try
            {
                var bot = new TelegramBotClient("کلید API شما");
                id += 7;
                //var photoStream = new FileStream(imagePath, FileMode.Open);
                //var t = await bot.SendPhotoAsync("@channelname یا chat_id", new InputOnlineFile(photoStream), caption);
                // ارسال تصویر به کانال (با نام کاربری یا chat_id)
                var photoStream = new FileStream(imagePath, FileMode.Open);
                var photo = new InputFileStream(photoStream, "image.jpg");

                var t = await bot.SendPhotoAsync("@channelname یا chat_id", photo, (Int32)id, caption);
                return 200;
            }
            catch (Exception e)
            {
                return 500;
            }
        }
        public async Task<int> SendMessageToTelegram(string channelName, string caption, string token)
        {

            try
            {

                // کلید API را در اینجا قرار دهید
                var bot = new TelegramBotClient("کلید API شما");

                // ارسال پیام متنی به کانال (با نام کاربری یا chat_id)
                var t = await bot.SendTextMessageAsync("@channelname یا chat_id", "متن پیام");
                return 200;
            }
            catch (Exception e)
            {
                return 500;
            }
        }
        public async Task<int> DeleteTelegram(int postId)
        {
            try
            {
                // نمونه کد C# برای حذف پیام از تلگرام
                var chatId = "CHAT_ID"; // شناسه چت
                var messageId = "MESSAGE_ID"; // شناسه پیام
                var bot = new TelegramBotClient("YOUR_BOT_TOKEN");
                await bot.DeleteMessageAsync(chatId, postId);
                return 200;
            }
            catch (Exception e)
            {
                return 500;
            }
        }
        public async Task<int> Delete(long id)
        {
            try
            {
                var Post = await Context.Telegrams.FirstOrDefaultAsync(i => i.Id.Equals(id));
                if (Post == null)
                    return 404;
                Context.Telegrams.Remove(Post);
                return 200;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 500;
            }
        }




    }
}
