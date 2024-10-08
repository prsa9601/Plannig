using Domain.SocialMediaAgg.InstagramAgg;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Infrastructure._Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent.Ef.InstagramAgg
{
    internal class InstagramRepository : BaseRepository<Instagram>, IInstagramRepository
    {
        public InstagramRepository(PlanningContext context) : base(context)
        {
        }
        private readonly HttpClient client = new HttpClient();

        public async Task<int> PostToInstagram(string accessToken, string imageUrl, string caption)
        {

            var endpoint = $"https://graph.instagram.com/v1.0/me/media?access_token={accessToken}";
            //List<MultipartFormDataContent> content = new List<MultipartFormDataContent>();
            var content = new MultipartFormDataContent
            {
                { new StringContent("png"), "media_type" },
                { new StringContent(imageUrl), "image_url" },
                { new StringContent(caption), "caption" }
            };
            //foreach (var item in imageUrl)
            //{
            //    var model = new MultipartFormDataContent()
            //    {
            //        { new StringContent("IMAGE"), "media_type" },
            //        { new StringContent(item), "image_url" },
            //        { new StringContent(caption), "caption" }
            //    };
            //    content.Add(model);
            //}
            var response = await client.PostAsync(endpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return 200;
        }
        public async Task<int> DeleteStory(string InstagramId, int postId, string accessToken, string imagePath, string token)
        {
            try
            {
                var client = new HttpClient();
                var requestContent = new MultipartFormDataContent();
                requestContent.Add(new StringContent(accessToken), "access_token");
                requestContent.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(imagePath)), "file", "story.jpg");

                var response = await client.DeleteAsync($"https://graph.instagram.com/v1.0/me/media?access_token={accessToken}&post_id= {postId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Handle the response
                    return 200;
                }
                else
                {
                    // Handle the error
                    return 500;
                }

            }
            catch (Exception e)
            {
                return 500;
            }
        }

        public async Task<int> UploadStory(string InstagramId, string accessToken, string imagePath, string token)
        {
            var client = new HttpClient();
            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(accessToken), "access_token");
            requestContent.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(imagePath)), "file", "story.jpg");

            var response = await client.PostAsync("https://graph.instagram.com/v1.0/me/media", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Handle the response
                return 200;
            }
            else
            {
                // Handle the error
                return 500;
            }
        }

        public async Task<Instagram?> GetTrackingByUserName(string userName)
        {
            var result = await Context.Instagram.FirstOrDefaultAsync(i=>i.UserName == userName);
            return result;
        }
        public async Task<Instagram?> GetTrackingByInstagramId(string id)
        {
            var result = await Context.Instagram.FirstOrDefaultAsync(i=>i.InstagramId == id);
            return result;
        }

        public async Task<int> Delete(long id)
        {
            try
            {
                var Post = await Context.Instagram.FirstOrDefaultAsync(i => i.Id.Equals(id));
                if (Post == null)
                    return 404;
                Context.Instagram.Remove(Post);
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
