using Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;
using NReco.VideoInfo;
using Telegram.Bot.Types;
using Telegram.Bot;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using System.Net;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Persistent.Ef.TelegramAgg
{
    public class TelegramRepository : BaseRepository<Domain.SocialMediaAgg.TelegramAgg.Telegram>, ITelegramRepository
    {
        public TelegramRepository(PlanningContext context) : base(context)
        {
        }
        public async Task<int> SendVideoToTelegram(string channelName,
            string videoCaption, string videoPath, long id,
            int width, int height, string thumbnail, string token)
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
        public async Task<int> SendImageToTelegram(string channelName, string caption,
            List<string> imagePaths, long id, string token)
        {
            try
            {
                var bot = new TelegramBotClient("کلید API شما");
                id += 7;
                //var photoStream = new FileStream(imagePath, FileMode.Open);
                //var t = await bot.SendPhotoAsync("@channelname یا chat_id", new InputOnlineFile(photoStream), caption);
                // ارسال تصویر به کانال (با نام کاربری یا chat_id)
                foreach (var imagePath in imagePaths)
                {
                    using var photoStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    var photo = new InputFileStream(photoStream, Path.GetFileName(imagePath));

                    await bot.SendPhotoAsync(channelName, photo, (int)id, caption);
                }
                //var photoStream = new FileStream(imagePath, FileMode.Open);
                //var photo = new InputFileStream(photoStream, "image.jpg");

                //var t = await bot.SendPhotoAsync("@channelname یا chat_id", photo, (Int32)id, caption);
                return 200;
            }
            catch (Exception e)
            {
                return 500;
            }
        }
        public async Task<int> SendMessageToTelegram(string channelName,
            string caption, string token)
        {
            try
            {
            //https://api.telegram.org/bot123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11/getMe
                var botClient = new TelegramBotClient
                    ("7503920971:AAEssb5Kmr4IY1YrAPKeqdeV6IhCawlodRM");
                //var botClient = new TelegramBotClient(token);
                //botId 7503920971
                var r = botClient.SendTextMessageAsync(chatId: "@PlanningTest",text: "g");
                
                // ارسال پیام به کانال
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId: channelName , // یا آیدی عددی کانال (مثال: -100123456789)
                    text: caption
                );
                // ارسال پیام متنی به کانال (با نام کاربری یا chat_id)
                //var t = await bot.SendTextMessageAsync($"{channelName}, {token}, {caption});
                //var t = await bot.SendTextMessageAsync("@channelname یا chat_id", "متن پیام");
                return (int)HttpStatusCode.OK;
            }
            catch (ApiRequestException ex) when (ex.Message.Contains("Forbidden"))
            {
                Console.WriteLine($"Error: Bot is not admin in channel {channelName}");
                return (int)HttpStatusCode.Forbidden; // 403
            }
            catch (ApiRequestException ex)
            {
                Console.WriteLine($"Telegram API Error: {ex.ErrorCode} - {ex.Message}");
                return (int)HttpStatusCode.BadRequest; // 400
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return (int)HttpStatusCode.InternalServerError; // 500
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
        //public async Task<int> Delete(long id)
        //{
        //    try
        //    {
        //        var Post = await Context.Telegrams.FirstOrDefaultAsync(i => i.Id.Equals(id));
        //        if (Post == null)
        //            return 404;
        //        Context.Telegrams.Remove(Post);
        //        return 200;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return 500;
        //    }
        //}


    }
}
