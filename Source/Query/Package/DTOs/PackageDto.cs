using Common.Query;
using static Domain.PackageAgg.Package;

namespace Query.Package.DTOs
{
    public class PackageDto : BaseDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Link { get; set; }
        public int Price { get; set; }
        public ExpiryTime ExpiryTime { get; set; }
        public int AllowedEmailCount { get; set; }
        public int AllowedSmsCount { get; set; }
        public List<PackageSpecificationDto> Specification { get; set; }
        public bool Active { get; set; } = false;

    }
   
    public class PackageDtoForUserProfile : BaseDto
    {
        public string Title { get; set; }
        public long UserPacakgeId { get; set; }
        public string ImageName { get; set; }
        public string Link { get; set; }
        public int Price { get; set; }
        public ExpiryTime ExpiryTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int AllowedEmailCount { get; set; }
        public int AllowedSmsCount { get; set; }
        public List<PackageSpecificationDto> Specification { get; set; }
        public bool Active { get; set; } = false;

    }
   
    public class PackageSpecificationDto : BaseDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public long PackageId { get; set; }
    }
}
