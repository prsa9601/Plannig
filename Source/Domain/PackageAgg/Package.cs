using System.Data;
using Common.Domain;
using Common.Domain.Exceptions;
using Domain.PackageAgg.Service;

namespace Domain.PackageAgg
{
    public class Package : BaseEntity
    {
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string Link { get; private set; }
        public int Price { get; private set; }
        public List<PackageSpecification> Specification { get; private set; }

        private Package()
        {
            
        }
        public Package(int price, string title, string imageName, string link, IPackageService _service)
        {
            Guard(title, _service);
            Title = title;
            ImageName = imageName;
            Link = link;
            Price = price;
        }

        public void Edit(int price, string title, string link, IPackageService _service)
        {
            Guard(title, _service);
            Title = title;
            Link = link;
            Price = price;
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

    }
    public class PackageSpecification : BaseEntity
    {
        public PackageSpecification(string key, string value)
        {
            NullOrEmptyDomainDataException.CheckString(key, nameof(key));
            NullOrEmptyDomainDataException.CheckString(value, nameof(value));

            Key = key;
            Value = value;
        }

        public long PackageId { get; internal set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}
