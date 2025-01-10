using Common.Domain;

namespace Domain.UserAgg;

public class RequestBox : BaseEntity
{
    private RequestBox()
    {
        
    }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string Description { get; set; } = $"";

    public string Title { get; set; } = "";
    //public long CurrentUserId { get; set; }
    public RequestBox(  string receiverId, string senderUserName)
    {
        Description = $" {senderUserName} به شما درخواست دوستی به شما داده است. \n برای اینکه {senderUserName} به دوستانتان اضافه شود روی دوست کلیک کنید.";
        Title = $"درخواست دوستی از {senderUserName}" ;
        ReceiverId = receiverId;
    }
    //public void AddFriend(List<string> friendsId)
    //{
    //    List<UserFriends> friendsList = new List<UserFriends>();

    //    foreach (var item in friendsId)
    //    {
    //        friendsList.Add(new UserFriends(item));
    //    }
    //    friends.ForEach(f => f.CurrentUserId = Id);

    //    friends.Clear();
    //    friends.AddRange(friendsList);
    //}
    //public void AddFriend(string friendId)
    //{

    //    var friend = new UserFriends(friendId);

    //    friend.CurrentUserId = Id;


    //    friends.Add(friend);
    //}
    //public void RemoveFriend(string friendId)
    //{

    //    var friend = friends.Where(f => f.CurrentUserId == Id && f.UserFriend == friendId).FirstOrDefault();
    //    friends.Remove(friend);

    //}
}