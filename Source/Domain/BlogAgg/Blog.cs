
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Domain.BlogAgg.Service;

namespace Domain.BlogAgg
{
    public class Blog : BaseEntity
    {
        public Blog(DateTime sendTime, string title, string description,
            string creatorUserName, SeoData seoData, string imageName,
            bool isSend, string slug, long categoryId, IBlogService service)
        {
            SendTime = sendTime;
            Title = title;
            Description = description;
            CreatorUserName = creatorUserName;
            SeoData = seoData;
            ImageName = imageName;
            IsSend = isSend;
            Slug = slug;
            CategoryId = categoryId;
        }
        private Blog()
        {

        }
        public string Slug { get; private set; }
        public string ImageName { get; private set; }
        public DateTime SendTime { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string CreatorUserName { get; private set; }
        public SeoData SeoData { get; private set; }
        public bool IsSend { get; private set; }
        public int Visit { get; private set; }
        public long CategoryId { get; set; }

        public void Edit(DateTime sendTime, string title, string description,
            string creatorUserName, SeoData seoData,
            bool isSend, string slug,long categoryId, IBlogService service)
        {
            //ImageName = imageName;
            SendTime = sendTime;
            Title = title;
            Description = description;
            CreatorUserName = creatorUserName;
            IsSend = isSend;
            SeoData = seoData;
            Slug = slug;
            CategoryId = categoryId;
        }
        public void IncreaseVisit()
        {
            Visit++;
        }
        public void SetImage(string imageName)
        {
            ImageName = imageName;
        }
        private void Guard(string slug, IBlogService service)
        {
            if (service.SlugExist(slug))
                throw new SlugIsDuplicateException();
        }
    }
}
