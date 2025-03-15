using Common.Domain;

namespace Domain.UserAgg
{
    public class UserPackage : BaseEntity
    {
        public UserPackage(long packageId, int allowedSmsCount, int allowedEmailCount, DateTime expiryDate, string packageTitle)
        {
            PackageId = packageId;
            AllowedSmsCount = allowedSmsCount;
            AllowedEmailCount = allowedEmailCount;
            ExpiryDate = expiryDate;
            PackageTitle = packageTitle;
        }

        public long PackageId { get; set; }
        public string PackageTitle { get; set; }
        public string UserId { get; set; }
        public int AllowedEmailCount { get; set; } = 10;
        public int AllowedSmsCount { get; set; } = 0;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public void Edit(DateTime expireDate, int allowedSmsCount, int allowedEmailCount)
        {
            ExpiryDate = expireDate;
            AllowedSmsCount = allowedSmsCount;
            AllowedEmailCount = allowedEmailCount;
        }
    }
}
