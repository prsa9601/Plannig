using Common.Domain;

namespace Domain.UserAgg
{
    public class UserAvatar : BaseEntity
    {
        public UserAvatar(Avatar AVATAR)
        {
            this.avatar = AVATAR;
        }

        private UserAvatar()
        {
            
        }
        public string UserId { get; internal set; }
        public Avatar avatar { get; private set; }
        public enum Avatar
        {
            Default,
            Man,
            Woman,
            Girl,
            Boy
        }

    }
}
