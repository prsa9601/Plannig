using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Domain.SocialMediaAgg.InstagramAgg.Service;
using System.Net.Http;
using System.Text.Json;

namespace Application.SocialMedia.Instagram
{
    public class InstagramService : IInstagramService
    {
        private readonly IInstagramRepository _repository;
        private readonly HttpClient _httpClient;

        public InstagramService(IInstagramRepository repository, HttpClient httpClient)
        {
            _repository = repository;
            _httpClient = httpClient;
        }

        public async Task<bool> InstagramAccountExist(string userName, string accessToken)
        {
            try
            {
                // ساختن URL درخواست
                var url = $"https://graph.instagram.com/v1/users/search?q={userName}&access_token={accessToken}";

                // ارسال درخواست GET به API
                var response = await _httpClient.GetAsync(url);

                // بررسی موفقیت‌آمیز بودن درخواست
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // پردازش پاسخ JSON
                    var jsonDocument = JsonDocument.Parse(responseContent);
                    var accounts = jsonDocument.RootElement.GetProperty("data");

                    // بررسی اگر حسابی موجود باشد
                    return accounts.GetArrayLength() > 0;
                }
                else
                {
                    // مدیریت خطا در درخواست
                    return false;
                }
            }
            catch (Exception ex)
            {
                // مدیریت خطاهای عمومی
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public Task<bool> InstagramPageExist(string pageId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InstagramExistInDataBase(string instagramId, string pageId)
        {
            return await _repository.ExistsAsync(i => i.InstagramId.Equals(instagramId) 
            || i.PageId.Equals(pageId));
        }
    }
}
