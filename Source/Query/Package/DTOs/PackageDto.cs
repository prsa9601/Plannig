using Common.Query;
using Domain.PackageAgg;

namespace Query.Package.DTOs
{
    public class PackageDto : BaseDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Link { get; set; }
        public int Price { get; set; }
        public List<PackageSpecificationDto> Specification { get; set; }

    }

    public class PackageSpecificationDto : BaseDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public long PackageId { get; set; }
    }
}
