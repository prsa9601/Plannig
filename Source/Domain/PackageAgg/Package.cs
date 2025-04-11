using System.Data;
using Common.Domain;
using Common.Domain.Exceptions;
using Domain.PackageAgg.Service;
using Domain.RoleAgg;
using Domain.UserAgg;

namespace Domain.PackageAgg
{
    public partial class Package : BaseEntity
    {
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string Link { get; private set; }
        public bool Active { get; set; } = false;  //اینکه تو سایت باشه یا نه
        public int Price { get; private set; }
        public int AllowedEmailCount { get; private set; }
        public int AllowedSmsCount { get; private set; }
        public int AllowedPostTelegram { get; set; } = 10;
        public int AllowedPostInstagram { get; set; } = 10;
        public int AllowedStoryInstagram { get; set; } = 10;
        public ExpiryTime ExpiryDate { get; set; }

        public List<PackageSpecification> Specification { get; set; }

        private Package()
        {

        }
        public Package(ExpiryTime expiryTime, int allowedSmsCount,
            int allowedEmailCount, int price, string title, string imageName, 
            string link, int allowedPostTelegram, int allowedPostInstagram, 
            int allowedStoryInstagram, IPackageService _service)
        {
            Guard(title, _service);
            Title = title;
            ImageName = imageName;
            Link = link;
            Price = price;
            AllowedEmailCount = allowedSmsCount;
            AllowedSmsCount = allowedEmailCount;
            ExpiryDate = expiryTime;

            AllowedPostInstagram = allowedPostInstagram;
            AllowedStoryInstagram = allowedStoryInstagram;
            AllowedPostTelegram = allowedPostTelegram;
        }

        public void Edit(ExpiryTime expiryTime, int allowedSmsCount, int allowedEmailCount, 
            int price, string title, string link, int allowedPostTelegram, int allowedPostInstagram,
            int allowedStoryInstagram, IPackageService _service)
        {
            Title = title;
            Link = link;
            Price = price;
            AllowedEmailCount = allowedSmsCount;
            AllowedSmsCount = allowedEmailCount;
            ExpiryDate = expiryTime;

            AllowedPostInstagram = allowedPostInstagram;
            AllowedStoryInstagram = allowedStoryInstagram;
            AllowedPostTelegram = allowedPostTelegram;
        }

        public void SetImage(string imageName)
        {
            ImageName = imageName;
        }
        public void Guard(string title, IPackageService _service)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));

            if (_service.ExistTitle(title))
                throw new DuplicateNameException($"this {title} has already been used");
        }
        public void SetSpecification(List<PackageSpecification> specifications)
        {
            specifications.ForEach(s => s.PackageId = Id);

            Specification = specifications;
        }

        public void SetActivePackage()
        {
            Active = true;
        }
        public void RemoveActivePackage()
        {
            Active = false;
        }
     
    }
}
